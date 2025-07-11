using DrugPrevention.Repositories.NganVHH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPrevention.Services.NganVHH
{
    public interface IConsultantsTrongLHService
    {
        Task<List<ConsultantsTrongLH>> GetAllAsync();
    }
}
