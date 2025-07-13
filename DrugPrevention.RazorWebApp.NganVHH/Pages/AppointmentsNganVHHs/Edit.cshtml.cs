using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DrugPrevention.Repositories.NganVHH.Models;
using DrugPrevention.Services.NganVHH;
using Microsoft.AspNetCore.Authorization;

namespace DrugPrevention.RazorWebApp.NganVHH.Pages.AppointmentsNganVHHs
{
    [Authorize(Roles = "1, 2")]
    public class EditModel : PageModel
    {
        private readonly IAppointmentsNganVHHService _appointmentsNganVHHService;
        private readonly IConsultantsTrongLHService _consultantsTrongLHService;
        private readonly IUsersTuyenTMService _usersTuyenTMService;


        public EditModel(IAppointmentsNganVHHService appointmentsNganVHHService, IConsultantsTrongLHService consultantsTrongLHService, IUsersTuyenTMService usersTuyenTMService)
        {
            _appointmentsNganVHHService = appointmentsNganVHHService;
            _consultantsTrongLHService = consultantsTrongLHService;
            _usersTuyenTMService = usersTuyenTMService;
        }

        // Bind Property : dùng cho trang html truy cập vào để có thể có được đối tượng mà show ra
        [BindProperty]
        public AppointmentsNganVHH AppointmentsNganVHH { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentsnganvhh = await _appointmentsNganVHHService.GetByIdAsync(id.Value);
            if (appointmentsnganvhh == null)
            {
                return NotFound();
            }
            AppointmentsNganVHH = appointmentsnganvhh;

            // Retrieve the list of users and consultants for dropdowns
            var appointmentUsers = await _usersTuyenTMService.GetAllAsync();
            appointmentUsers = appointmentUsers.Where(u => u.Role == "Member").ToList();
            var appointmentUserList = appointmentUsers.Select(u => new
            {
                u.UserTuyenTMID, // giá trị value
                DisplayText = $"{u.UserTuyenTMID} - {u.FirstName} {u.LastName} - {u.Email}" // giá trị hiển thị text
            }).ToList();

            var consultants = await _consultantsTrongLHService.GetAllAsync();
            consultants = consultants.Where(c => c.User.Role == "Consultant").ToList();
            var consultantList = consultants.Select(c => new
            {
                c.ConsultantTrongLHID, // giá trị value
                DisplayText = $"{c.ConsultantTrongLHID} - {c.User.FirstName} {c.User.LastName} - {c.Specialization} - {c.User.Email}" // giá trị hiển thị text
            }).ToList();

            // Populate the ViewData for dropdowns
            ViewData["ConsultantID"] = new SelectList(consultantList, "ConsultantTrongLHID", "DisplayText");
            ViewData["UserID"] = new SelectList(appointmentUserList, "UserTuyenTMID", "DisplayText");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _appointmentsNganVHHService.UpdateAsync(AppointmentsNganVHH);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentsNganVHHExists(AppointmentsNganVHH.AppointmentNganVHHID).Result)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> AppointmentsNganVHHExists(int id)
        {
            var result = await _appointmentsNganVHHService.GetByIdAsync(id); 

            return result != null && result.AppointmentNganVHHID == id;
        }
    }
}
