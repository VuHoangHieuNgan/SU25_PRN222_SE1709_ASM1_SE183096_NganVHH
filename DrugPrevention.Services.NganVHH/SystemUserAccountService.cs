using DrugPrevention.Repositories.NganVHH.Models;
using DrugPrevention.Repositories.NganVHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPrevention.Services.NganVHH
{
    public class SystemUserAccountService : ISystemUserAccountService
    {
        private readonly SystemUserAccountRepository _repository;
        public SystemUserAccountService() => _repository ??= new SystemUserAccountRepository();

        public async Task<System_UserAccount> GetUserAccountAsync(string userName, string password)
        {
            return await _repository.GetUserAccount(userName, password);
        }

        public async Task<System_UserAccount> GetUserByEmailAsync(string email)
        {
            return await _repository.GetUserByEmailAsync(email);
        }

        public async Task<int> CreateUserAccountAsync(System_UserAccount userAccount)
        {
            return await _repository.CreateUserAccountAsync(userAccount);
        }
    }
}
