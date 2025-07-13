using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DrugPrevention.Repositories.NganVHH.Models;
using DrugPrevention.Services.NganVHH;
using Microsoft.AspNetCore.Authorization;

namespace DrugPrevention.RazorWebApp.NganVHH.Pages.AppointmentsNganVHHs
{
    [Authorize(Roles = "1, 2")]
    public class CreateModel : PageModel
    {
        private readonly IAppointmentsNganVHHService _appointmentsNganVHHService;
        private readonly IConsultantsTrongLHService _consultantsTrongLHService;
        private readonly IUsersTuyenTMService _usersTuyenTMService;

        public CreateModel(IAppointmentsNganVHHService appointmentsNganVHHService, IConsultantsTrongLHService consultantsTrongLHService, IUsersTuyenTMService usersTuyenTMService)
        {
            _appointmentsNganVHHService = appointmentsNganVHHService;
            _consultantsTrongLHService = consultantsTrongLHService;
            _usersTuyenTMService = usersTuyenTMService;
        }

        [BindProperty]
        public AppointmentsNganVHH AppointmentsNganVHH { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            AppointmentsNganVHH = new AppointmentsNganVHH
            {
                AppointmentDateTime = DateTime.Now.AddDays(1),
                Duration = 60,
                Status = "Scheduled",
                IsInterpreterNeeded = false,
                ConsultationType = "Online",
                RiskLevel = "Low"
            };

            await LoadDropdownData();

            return Page();
        }



        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Reload dropdown data khi có lỗi validation
                await LoadDropdownData();
                return Page();
            }

            await _appointmentsNganVHHService.CreateAsync(AppointmentsNganVHH);

            return RedirectToPage("./Index");
        }

        private async Task LoadDropdownData()
        {
            var appointmentUsers = await _usersTuyenTMService.GetAllAsync();
            appointmentUsers = appointmentUsers.Where(u => u.Role == "Member").ToList();
            var appointmentUserList = appointmentUsers.Select(u => new
            {
                Value = u.UserTuyenTMID,
                Text = $"ID: {u.UserTuyenTMID} - {CleanString(u.FirstName)} {CleanString(u.LastName)} - {CleanString(u.Email)}"
            }).ToList();

            ViewData["UserID"] = new SelectList(appointmentUserList, "Value", "Text");


            var consultants = await _consultantsTrongLHService.GetAllAsync();

            consultants = consultants.Where(c => c.User.Role == "Consultant").ToList();
            var consultantList = consultants.Select(c => new
            {
                Value = c.ConsultantTrongLHID,
                Text = $"ID: {c.ConsultantTrongLHID} - {CleanString(c.User.FirstName)} {CleanString(c.User.LastName)} - {CleanString(c.Specialization)} - {CleanString(c.User.Email)}"
            }).ToList();

            ViewData["ConsultantID"] = new SelectList(consultantList, "Value", "Text");
        }

        private string CleanString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "N/A";

            // Loại bỏ các ký tự đặc biệt có thể gây lỗi encoding
            return input.Trim().Replace("�", "").Replace("?", "");
        }
    }
}
