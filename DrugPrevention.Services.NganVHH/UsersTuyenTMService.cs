using DrugPrevention.Repositories.NganVHH.Models;
using DrugPrevention.Repositories.NganVHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPrevention.Services.NganVHH
{
    public class UsersTuyenTMService : IUsersTuyenTMService
    {
        private readonly UsersTuyenTMRepository _repository;

        public UsersTuyenTMService() => _repository ??= new UsersTuyenTMRepository();

        public async Task<List<UsersTuyenTM>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
