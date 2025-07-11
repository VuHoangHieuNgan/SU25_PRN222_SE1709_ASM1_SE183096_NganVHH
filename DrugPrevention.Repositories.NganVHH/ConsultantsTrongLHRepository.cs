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
    public class ConsultantsTrongLHRepository : GenericRepository<ConsultantsTrongLH>
    {
        public ConsultantsTrongLHRepository(){}

        public ConsultantsTrongLHRepository(SU25_PRN222_SE1709_G2_DrugPreventionSystemContext context) => _context = context;

        public async Task<List<ConsultantsTrongLH>> GetAllAsync()
        {
            return await _context.ConsultantsTrongLHs
                .Include(c => c.User) // Include User navigation property
                .ToListAsync();
        }
    }
}
