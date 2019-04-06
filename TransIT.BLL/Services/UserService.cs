using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services
{
    public class UserService : IUserService
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IUserRepository _userRepository;
    
        public UserService(
            IUnitOfWork uow,
            IUserRepository userRepository)
        {
            _uow = uow;
            _userRepository = userRepository;
        }

        public Task<User> Get(int userId) =>
            _userRepository.GetByIdAsync(userId);

        public Task<IEnumerable<User>> Get(uint index, uint amount) =>
            _userRepository.GetRangeAsync(index, amount);

        public async Task<bool> Create(User value)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Modify(User value)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Delete(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}