using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TransIT.BLL.Security.Hashers;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// User model CRUD
    /// </summary>
    /// <see cref="IUserService"/>

    public class UserService : CrudService<User>, IUserService
    {
        /// <summary>
        /// Manages password hashing
        /// </summary>
        protected IPasswordHasher _hasher;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public UserService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<User>> logger,
            IUserRepository repository,
            IPasswordHasher hasher) : base(unitOfWork, logger, repository)
        {
            _hasher = hasher;
        }

        /// <summary>
        /// Creates user if login and password not empty and does not exist in DB
        /// hashes password and set zero to id
        /// </summary>
        /// <see cref="IPasswordHasher.HashPassword(string)"/>
        /// <param name="user">User model</param>
        /// <returns>Is successful</returns>
        public override async Task<User> CreateAsync(User user)
        {
            var role = user.Role.Name.ToUpper();
            user.Role = (await GetRolesByName(role)).SingleOrDefault();

            if (user.Role == null) return null;

            user.Id = 0;
            user.Password = _hasher.HashPassword(user.Password);

            return await base.CreateAsync(user);
        }

        private Task<IEnumerable<Role>> GetRolesByName(string name) =>
            _unitOfWork.RoleRepository.GetAllAsync(r => r.Name == name);
        
        public override Task<IEnumerable<User>> SearchAsync(string search)
        {
            search = search.ToUpperInvariant();
            try
            {
                return _unitOfWork.UserRepository.GetAllAsync(a =>
                    a.Login.ToUpperInvariant().Contains(search)
                    || a.Email.ToUpperInvariant().Contains(search)
                    || a.PhoneNumber.ToUpperInvariant().Contains(search)
                    || a.LastName.ToUpperInvariant().Contains(search)
                    || a.FirstName.ToUpperInvariant().Contains(search)
                    || search.Contains(a.Login.ToUpperInvariant())
                    || search.Contains(a.Email.ToUpperInvariant())
                    || search.Contains(a.PhoneNumber.ToUpperInvariant())
                    || search.Contains(a.LastName.ToUpperInvariant())
                    || search.Contains(a.FirstName.ToUpperInvariant()));
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(SearchAsync));
                return null;
            }
        }
    }
}
