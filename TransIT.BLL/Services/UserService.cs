using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// User repo
        /// </summary>
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// Roles repo
        /// </summary>
        private readonly IRoleRepository _roleRepository;
        /// <summary>
        /// Hasher for password
        /// </summary>
        private readonly IPasswordHasher _hasher;
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<UserService> _logger;
        
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger">Logs errors</param>
        /// <param name="hasher">Used to hash password</param>
        /// <param name="unitOfWork">Used to save changes</param>
        /// <param name="userRepository">Used to CRUD users</param>
        /// <param name="roleRepository">Used to retrieve roles</param>
        public UserService(
            ILogger<UserService> logger,
            IPasswordHasher hasher,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _hasher = hasher;
            _logger = logger;
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
        /// <param name="offset">Where to start</param>
        /// <param name="amount">Amount to give</param>
        /// <returns>List of users</returns>
        public Task<IEnumerable<User>> Get(uint offset, uint amount) =>
            _userRepository.GetRangeAsync(offset, amount);

        /// <summary>
        /// Creates user if login and password not empty and does not exist in DB
        /// hashes password and set zero to id
        /// </summary>
        /// <see cref="IPasswordHasher.HashPassword(string)"/>
        /// <param name="user">User model</param>
        /// <returns>Is successful</returns>
        public async Task<User> Create(User user)
        {
            if ((await _userRepository.GetAllAsync(u =>
                u.Login == user.Login)).Any()) 
                return null;
            
            var role = user.Role.Name.ToUpper();
            user.Role = (await _roleRepository
                .GetAllAsync(r => r.Name == role))
                .SingleOrDefault();

            if (user.Role == null) return null;

            user.Id = 0;
            user.Password = _hasher.HashPassword(user.Password);

            try
            {
                var res = await _userRepository.AddAsync(user);
                await _unitOfWork.SaveAsync();
                return res.Entity;
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, nameof(Create), e.Entries);
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(Create));
                throw e;
            }
        }

        /// <summary>
        /// Updates user if it is found
        /// </summary>
        /// <param name="user">User to update</param>
        /// <returns>Is successful</returns>
        public async Task<User> Update(User user)
        {
            if (await _userRepository.GetByIdAsync(user.Id) == null)
                return null;

            try
            {
                var res = _userRepository.Update(user);
                await _unitOfWork.SaveAsync();
                return res.Entity;
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, nameof(Update), e.Entries);
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(Update));
                throw e;
            }
        }

        /// <summary>
        /// Removes a user in DB
        /// </summary>
        /// <param name="userId">Id of user to delete</param>
        /// <returns>Is successful</returns>
        public async Task Delete(int userId)
        {
            try
            {
                _userRepository.Remove(new User {Id = userId});
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, nameof(Delete), e.Entries);
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(Delete));
                throw e;
            }
        }
    }
}
