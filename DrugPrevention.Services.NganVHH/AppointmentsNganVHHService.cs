using DrugPrevention.Repositories.NganVHH.Models;
using DrugPrevention.Repositories.NganVHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPrevention.Services.NganVHH
{
    public class AppointmentsNganVHHService : IAppointmentsNganVHHService
    {
        private readonly AppointmentsNganVHHRepository _repository;

        public AppointmentsNganVHHService() => _repository ??= new AppointmentsNganVHHRepository();


        public async Task<List<AppointmentsNganVHH>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<AppointmentsNganVHH> GetByIdAsync(int code)
        {
            return await _repository.GetByIdAsync(code);
        }

        public async Task<List<AppointmentsNganVHH>> SearchAsync(string primaryIssue, int? duration, string specialization)
        {
            return await _repository.SearchAsync(primaryIssue, duration, specialization);
        }

        // Pagination methods
        public async Task<List<AppointmentsNganVHH>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _repository.GetAllAsync(pageNumber, pageSize);
        }

        public async Task<List<AppointmentsNganVHH>> SearchAsync(string primaryIssue, int? duration, string specialization, int pageNumber, int pageSize)
        {
            return await _repository.SearchAsync(primaryIssue, duration, specialization, pageNumber, pageSize);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _repository.GetTotalCountAsync();
        }

        public async Task<int> GetSearchCountAsync(string primaryIssue, int? duration, string specialization)
        {
            return await _repository.GetSearchCountAsync(primaryIssue, duration, specialization);
        }
        public async Task<int> CreateAsync(AppointmentsNganVHH item)
        {
            return await _repository.CreateAsync(item);
        }

        public async Task<int> UpdateAsync(AppointmentsNganVHH item)
        {
            Console.WriteLine($"{item.AppointmentNganVHHID} - {item.AppointmentDateTime}");
            Console.WriteLine($"FeedbackRating value: {item.FeedbackRating}");
            var result = await _repository.UpdateAsync(item);
            await _repository.SaveAsync();
            return result;

        }

        public async Task<bool> DeleteAsync(int code)
        {
            var item = await _repository.GetByIdAsync(code);

            if (item == null)
            {
                return false;
            }

            return await _repository.RemoveAsync(item);
        }
    }
}
