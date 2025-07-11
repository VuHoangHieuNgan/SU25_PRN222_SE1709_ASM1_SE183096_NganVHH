using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DrugPrevention.Repositories.NganVHH.Models;
using DrugPrevention.Services.NganVHH;
using Microsoft.AspNetCore.Authorization;

namespace DrugPrevention.RazorWebApp.NganVHH.Pages.AppointmentsNganVHHs
{
    [Authorize(Roles = "1, 2")]
    public class DetailsModel : PageModel
    {
        private readonly IAppointmentsNganVHHService _appointmentsNganVHHService;

        public DetailsModel(IAppointmentsNganVHHService appointmentsNganVHHService)
        {
            _appointmentsNganVHHService = appointmentsNganVHHService;
        }

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
            else
            {
                AppointmentsNganVHH = appointmentsnganvhh;
            }
            return Page();
        }
    }
}
