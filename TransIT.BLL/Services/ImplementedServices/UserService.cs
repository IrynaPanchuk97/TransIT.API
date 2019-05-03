using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TransIT.API.Extensions;
using TransIT.BLL.Security.Hashers;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.DTOs;
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

        protected IRoleRepository _roleRepository;

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
            IRoleRepository roleRepository,
            IPasswordHasher hasher) : base(unitOfWork, logger, repository)
        {
            _hasher = hasher;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// Gets user by id and ensures that role is assigned
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <returns>User with id</returns>
        public async override Task<User> GetAsync(int id)
        {
            var user = await base.GetAsync(id);
            if (user.Role == null)
                user.Role = await _unitOfWork.RoleRepository
                    .GetByIdAsync((int)user.RoleId);
            return user;
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
            try
            {
                user.Id = 0;
                user.RoleId = (await _roleRepository.GetByIdAsync((int)user.RoleId)).Id;
                user.Role = null;
                user.Password = _hasher.HashPassword(user.Password);
                return await base.CreateAsync(user);
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

        public virtual async Task<User> UpdateAsync(User model, bool modifyPassword = false)
        {
            try
            {
                if (!modifyPassword)
                {
                    var res = _repository.UpdateWithIgnoreProperty(model, u => u.Password);
                    await _unitOfWork.SaveAsync();
                    return res;
                }

                return await base.UpdateAsync(model);
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

        public virtual async Task<IEnumerable<User>> GetAssignees(uint offset, uint amount) =>
            (await _repository.GetAllAsync())
            .AsQueryable()
            .Where(x => x.Role.Name == ROLE.WORKER)
            .Skip((int)offset)
            .Take((int)amount);

        private Task<IEnumerable<Role>> GetRolesByName(string name) =>
            _unitOfWork.RoleRepository.GetAllAsync(r => r.Name == name);
        
        protected override Task<IEnumerable<User>> SearchExpressionAsync(IEnumerable<string> strs) =>
            _unitOfWork.UserRepository.GetAllAsync(entity =>
                strs.Any(str => entity.Login.ToUpperInvariant().Contains(str)
                || entity.Email.ToUpperInvariant().Contains(str)
                || entity.PhoneNumber.ToUpperInvariant().Contains(str)
                || entity.LastName.ToUpperInvariant().Contains(str)
                || entity.FirstName.ToUpperInvariant().Contains(str)));
    }
}
