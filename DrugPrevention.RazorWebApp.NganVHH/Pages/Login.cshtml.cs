using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using DrugPrevention.Services.NganVHH;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication.Google;
using DrugPrevention.Repositories.NganVHH.Models;



namespace DrugPrevention.RazorWebApp.NganVHH.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly ISystemUserAccountService _userAccountService;

        public LoginModel(ISystemUserAccountService systemUserAccountService)
        {
            _userAccountService = systemUserAccountService;
        }

        [BindProperty]
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; } = string.Empty;


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
                       
            var userAccount = await _userAccountService.GetUserAccountAsync(UserName, Password);

            if (userAccount != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, UserName),
                    new Claim(ClaimTypes.Role, userAccount.RoleId.ToString()),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                Response.Cookies.Append("UserName", userAccount.UserName);

                //// After signing then redirect to default page
                return RedirectToPage("/AppointmentsNganVHHs/Index");
                //return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không chính xác.");
                TempData["Message"] = "Đăng nhập thất bại, vui lòng kiểm tra lại thông tin tài khoản.";
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Page();
        }

        public IActionResult OnGetGoogleLogin()
        {
            var properties = new AuthenticationProperties 
            { 
                RedirectUri = Url.Page("/Login", "GoogleResponse"),
                Items =
                {
                    { "scheme", GoogleDefaults.AuthenticationScheme }
                }
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> OnGetGoogleResponse()
        {
            try
            {
                var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
                if (!result.Succeeded)
                {
                    TempData["Message"] = "Đăng nhập Google thất bại.";
                    return Page();
                }

                var claims = result.Principal.Claims;
                var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(email))
                {
                    TempData["Message"] = "Không thể lấy thông tin email từ Google.";
                    return Page();
                }

                // Tạo user account mới hoặc lấy existing user
                var userName = email.Split('@')[0];
                var user = await CreateOrGetGoogleUser(email, name, userName);

                // Tạo claims để đăng nhập
                var claimsIdentity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.UserAccountID.ToString())
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                // Đăng nhập
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(30),
                    HttpOnly = true,
                    IsEssential = true
                };
                Response.Cookies.Append("UserEmail", email, cookieOptions);
                Response.Cookies.Append("UserName", user.UserName, cookieOptions);
                Response.Cookies.Append("UserId", user.UserAccountID.ToString(), cookieOptions);

                return RedirectToPage("/AppointmentsNganVHHs/Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Có lỗi xảy ra trong quá trình đăng nhập Google.";
                return Page();
            }
        }

        private async Task<System_UserAccount> CreateOrGetGoogleUser(string email, string name, string userName)
        {
            // Kiểm tra user đã tồn tại chưa dựa trên email
            var existingUser = await _userAccountService.GetUserByEmailAsync(email); 
            
            if (existingUser != null)
            {
                return existingUser;
            }

            // Tạo user mới
            var newUser = new System_UserAccount
            {
                UserName = userName,
                Email = email,
                FullName = name ?? userName,
                Password = Guid.NewGuid().ToString(), // Random password cho Google users
                Phone = "",
                EmployeeCode = userName,
                RoleId = 2, // Default role (có thể thay đổi theo logic của bạn)
                CreatedDate = DateTime.Now,
                IsActive = true,
                CreatedBy = "Google"
            };

            // Tạo user mới trong database
            await _userAccountService.CreateUserAccountAsync(newUser);
            
            return newUser;
        }
    }    
}
