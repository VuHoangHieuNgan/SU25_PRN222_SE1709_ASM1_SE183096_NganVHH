using DrugPrevention.Repositories.NganVHH.Basic;
using DrugPrevention.Repositories.NganVHH.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPrevention.Repositories.NganVHH
{
    public class AppointmentsNganVHHRepository : GenericRepository<AppointmentsNganVHH>
    {
        public AppointmentsNganVHHRepository() { }

        public AppointmentsNganVHHRepository(SU25_PRN222_SE1709_G2_DrugPreventionSystemContext context) => _context = context;
        public override async Task<List<AppointmentsNganVHH>> GetAllAsync()
        {
            return await _context.AppointmentsNganVHHs
                .Include(a => a.Consultant)
                    .ThenInclude(c => c.User)
                .Include(a => a.User)
                .ToListAsync() ?? new List<AppointmentsNganVHH>();
        }

        public override async Task<AppointmentsNganVHH> GetByIdAsync(int code) //(Guid code) 
        {
            var item = await _context.AppointmentsNganVHHs
                .Include(i => i.Consultant)
                    .ThenInclude(c => c.User)
                .Include(i => i.User)
                .FirstOrDefaultAsync(o => o.AppointmentNganVHHID == code);

            return item ?? new AppointmentsNganVHH();
        }

        //Search 3 trường thông tin
        public async Task<List<AppointmentsNganVHH>> SearchAsync(string primaryIssue, int? duration, string specialization)
        {
            var list = await _context.AppointmentsNganVHHs
                .Include(i => i.Consultant)
                    .ThenInclude(c => c.User)
                .Include(i => i.User)
                .Where(i =>
                    (i.PrimaryIssue.Contains(primaryIssue) || string.IsNullOrEmpty(primaryIssue))
                    && (i.Duration == duration || duration == null)
                    && (i.Consultant.Specialization.Contains(specialization) || string.IsNullOrEmpty(specialization))
                    ).ToListAsync();

            return list ?? new List<AppointmentsNganVHH>();
        }

        // Pagination methods
        public async Task<List<AppointmentsNganVHH>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.AppointmentsNganVHHs
                .Include(a => a.Consultant)
                    .ThenInclude(c => c.User)
                .Include(a => a.User)
                .OrderByDescending(a => a.AppointmentNganVHHID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync() ?? new List<AppointmentsNganVHH>();
        }

        public async Task<List<AppointmentsNganVHH>> SearchAsync(string primaryIssue, int? duration, string specialization, int pageNumber, int pageSize)
        {
            return await _context.AppointmentsNganVHHs
                .Include(i => i.Consultant)
                    .ThenInclude(c => c.User)
                .Include(i => i.User)
                .Where(i =>
                    (i.PrimaryIssue.Contains(primaryIssue) || string.IsNullOrEmpty(primaryIssue))
                    && (i.Duration == duration || duration == null)
                    && (i.Consultant.Specialization.Contains(specialization) || string.IsNullOrEmpty(specialization))
                )
                .OrderByDescending(a => a.AppointmentNganVHHID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync() ?? new List<AppointmentsNganVHH>();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.AppointmentsNganVHHs.CountAsync();
        }

        public async Task<int> GetSearchCountAsync(string primaryIssue, int? duration, string specialization)
        {
            return await _context.AppointmentsNganVHHs
                .Include(i => i.Consultant)
                .Where(i =>
                    (i.PrimaryIssue.Contains(primaryIssue) || string.IsNullOrEmpty(primaryIssue))
                    && (i.Duration == duration || duration == null)
                    && (i.Consultant.Specialization.Contains(specialization) || string.IsNullOrEmpty(specialization))
                )
                .CountAsync();
        }
    }
}
