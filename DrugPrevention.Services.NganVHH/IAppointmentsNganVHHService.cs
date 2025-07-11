using DrugPrevention.Repositories.NganVHH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPrevention.Services.NganVHH
{
    public interface IAppointmentsNganVHHService
    {
        Task<List<AppointmentsNganVHH>> GetAllAsync();
        Task<AppointmentsNganVHH> GetByIdAsync(int code);

        Task<List<AppointmentsNganVHH>> SearchAsync(string primaryIssue, int? duration, string specialization);

        // Pagination methods
        Task<List<AppointmentsNganVHH>> GetAllAsync(int pageNumber, int pageSize);
        Task<List<AppointmentsNganVHH>> SearchAsync(string primaryIssue, int? duration, string specialization, int pageNumber, int pageSize);
        Task<int> GetTotalCountAsync();
        Task<int> GetSearchCountAsync(string primaryIssue, int? duration, string specialization);

        Task<int> CreateAsync(AppointmentsNganVHH appointment);

        Task<int> UpdateAsync(AppointmentsNganVHH appointment);

        Task<bool> DeleteAsync(int code);
    }
}
