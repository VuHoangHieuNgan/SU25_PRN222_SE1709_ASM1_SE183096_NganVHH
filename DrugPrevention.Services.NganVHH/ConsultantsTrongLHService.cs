using DrugPrevention.Repositories.NganVHH;
using DrugPrevention.Repositories.NganVHH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPrevention.Services.NganVHH
{
    public class ConsultantsTrongLHService : IConsultantsTrongLHService
    {
        private readonly ConsultantsTrongLHRepository _repository;

        public ConsultantsTrongLHService() => _repository ??= new ConsultantsTrongLHRepository();

        public async Task<List<ConsultantsTrongLH>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
