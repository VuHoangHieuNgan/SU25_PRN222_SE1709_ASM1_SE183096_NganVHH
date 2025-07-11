using DrugPrevention.Repositories.NganVHH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPrevention.Services.NganVHH
{
    public interface ISystemUserAccountService
    {
        Task<System_UserAccount> GetUserAccountAsync(string userName, string password);
        Task<System_UserAccount> GetUserByEmailAsync(string email);
        Task<int> CreateUserAccountAsync(System_UserAccount userAccount);
    }
}
