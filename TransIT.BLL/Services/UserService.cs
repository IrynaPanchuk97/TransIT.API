using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransIT.BLL.Security.Hashers;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services
{
    /// <summary>
    /// User model CRUD
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// Saves changes
        /// </summary>
        protected readonly IUnitOfWork _uow;
        /// <summary>
        /// User repo
        /// </summary>
        protected readonly IUserRepository _userRepository;
        /// <summary>
        /// Roles repo
        /// </summary>
        protected readonly IRoleRepository _roleRepository;
        /// <summary>
        /// Hasher for password
        /// </summary>
        protected readonly IPasswordHasher _hasher;
        
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="hasher">Used to hash password</param>
        /// <param name="uow">Used to save changes</param>
        /// <param name="userRepository">Used to CRUD users</param>
        /// <param name="roleRepository">Used to retrieve roles</param>
        public UserService(
            IPasswordHasher hasher,
            IUnitOfWork uow,
            IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _uow = uow;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _hasher = hasher;
        }

        /// <summary>
        /// Finds user by id
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <returns>User or null unless not found</returns>
        public Task<User> Get(int userId) =>
            _userRepository.GetByIdAsync(userId);

        /// <summary>
        /// Gives users with pagination
        /// </summary>
        /// <param name="index">Where to start</param>
        /// <param name="amount">Amount to give</param>
        /// <returns>List of users</returns>
        public Task<IEnumerable<User>> Get(uint index, uint amount) =>
            _userRepository.GetRangeAsync(index, amount);

        /// <summary>
        /// Creates user if login and password not empty and does not exist in DB
        /// </summary>
        /// <param name="value">User model</param>
        /// <returns>Is successful</returns>
        public async Task<bool> Create(User value)
        {
            if (string.IsNullOrEmpty(value.Login)
                || string.IsNullOrEmpty(value.Password)
                || (await _userRepository.GetAllAsync(u =>
                u.Login == value.Login)).Any()) 
                return false;
            
            var role = value.Role.Name.ToUpper();
            value.Role = (await _roleRepository
                .GetAllAsync(r => r.Name == role))
                .SingleOrDefault();

            if (value.Role == null) return false;

            value.Id = 0;
            value.Password = _hasher.HashPassword(value.Password);
            
            await _userRepository.AddAsync(value);
            await _uow.SaveAsync();
            return true;
        }

        /// <summary>
        /// Updates user if it is found
        /// </summary>
        /// <param name="value">User to update</param>
        /// <returns>Is successful</returns>
        public async Task<bool> Update(User value)
        {
            if (await _userRepository.GetByIdAsync(value.Id) == null)
                return false;

            _userRepository.Update(value);
            await _uow.SaveAsync();
            return true;
        }

        /// <summary>
        /// Removes a user in DB
        /// </summary>
        /// <param name="userId">Id of user to delete</param>
        /// <returns>Is successful</returns>
        public async Task<bool> Delete(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;
            
            _userRepository.Remove(user);
            await _uow.SaveAsync();
            return true;
        }
    }
}