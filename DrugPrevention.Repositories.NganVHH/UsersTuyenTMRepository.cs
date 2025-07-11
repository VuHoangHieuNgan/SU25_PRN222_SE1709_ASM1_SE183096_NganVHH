using DrugPrevention.Repositories.NganVHH.Basic;
using DrugPrevention.Repositories.NganVHH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPrevention.Repositories.NganVHH
{
    public class UsersTuyenTMRepository : GenericRepository<UsersTuyenTM>
    {
        public UsersTuyenTMRepository() { }
        public UsersTuyenTMRepository(SU25_PRN222_SE1709_G2_DrugPreventionSystemContext context) => _context = context;
    }
}
