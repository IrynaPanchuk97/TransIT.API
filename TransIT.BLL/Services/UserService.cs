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
    public class UserService : ICrudService<User>
    {
        /// <summary>
        /// Saves changes
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
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
        public UserService(
            ILogger<UserService> logger,
            IPasswordHasher hasher,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _hasher = hasher;
            _logger = logger;
        }

        /// <summary>
        /// Finds user by id
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <returns>User or null unless not found</returns>
        public Task<User> GetAsync(int userId) =>
            _unitOfWork.UserRepository.GetByIdAsync(userId);

        /// <summary>
        /// Gives users with pagination
        /// </summary>
        /// <param name="offset">Where to start</param>
        /// <param name="amount">Amount to give</param>
        /// <returns>List of users</returns>
        public Task<IEnumerable<User>> GetAsync(uint offset, uint amount) =>
            _unitOfWork.UserRepository.GetRangeAsync(offset, amount);

        /// <summary>
        /// Creates user if login and password not empty and does not exist in DB
        /// hashes password and set zero to id
        /// </summary>
        /// <see cref="IPasswordHasher.HashPassword(string)"/>
        /// <param name="user">User model</param>
        /// <returns>Is successful</returns>
        public async Task<User> CreateAsync(User user)
        {
            if ((await _unitOfWork.UserRepository.GetAllAsync(u =>
                u.Login == user.Login)).Any()) 
                return null;
            
            var role = user.Role.Name.ToUpper();
            user.Role = (await _unitOfWork.RoleRepository
                .GetAllAsync(r => r.Name == role))
                .SingleOrDefault();

            if (user.Role == null) return null;

            user.Id = 0;
            user.Password = _hasher.HashPassword(user.Password);

            try
            {
                await _unitOfWork.UserRepository.AddAsync(user);
                await _unitOfWork.SaveAsync();
                return user;
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, nameof(CreateAsync), e.Entries);
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(CreateAsync));
                throw e;
            }
        }

        /// <summary>
        /// Updates user if it is found
        /// </summary>
        /// <param name="user">User to update</param>
        /// <returns>Is successful</returns>
        public async Task<User> UpdateAsync(User user)
        {
            if (await _unitOfWork.UserRepository.GetByIdAsync(user.Id) == null)
                return null;

            try
            {
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.SaveAsync();
                return user;
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, nameof(UpdateAsync), e.Entries);
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UpdateAsync));
                throw e;
            }
        }

        /// <summary>
        /// Removes a user in DB
        /// </summary>
        /// <param name="userId">Id of user to delete</param>
        /// <returns>Is successful</returns>
        public async Task DeleteAsync(int userId)
        {
            try
            {
                _unitOfWork.UserRepository.Remove(new User {Id = userId});
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, nameof(DeleteAsync), e.Entries);
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(DeleteAsync));
                throw e;
            }
        }
    }
}
