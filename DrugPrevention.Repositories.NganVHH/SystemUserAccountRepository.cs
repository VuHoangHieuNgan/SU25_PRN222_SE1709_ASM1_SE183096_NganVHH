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
    public class SystemUserAccountRepository : GenericRepository<System_UserAccount>
    {
        public SystemUserAccountRepository() { }
        public SystemUserAccountRepository(SU25_PRN222_SE1709_G2_DrugPreventionSystemContext context) => _context = context;

        public async Task<System_UserAccount> GetUserAccount(string username, string password)
        {
            return await _context.System_UserAccounts
                .FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);

            //return await _context.SystemUserAccounts
            //    .FirstOrDefaultAsync(u => u.Email == username && u.Password == password);

            //return await _context.SystemUserAccounts
            //    .FirstOrDefaultAsync(u => u.Email == username && u.Password == password && u.IsActive == true);

            //return await _context.SystemUserAccounts
            //    .FirstOrDefaultAsync(u => u.EmployeeCode == username && u.Password == password);

            //return await _context.SystemUserAccounts
            //    .FirstOrDefaultAsync(u => u.Phone == username && u.Password == password);
        }

        public async Task<System_UserAccount> GetUserByEmailAsync(string email)
        {
            return await _context.System_UserAccounts
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<int> CreateUserAccountAsync(System_UserAccount userAccount)
        {
            await _context.System_UserAccounts.AddAsync(userAccount);
            return await _context.SaveChangesAsync();
        }
    }
}
