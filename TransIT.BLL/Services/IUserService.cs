using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Services
{
    /// <summary>
    /// Set a behavior of entities which act with users
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets user by id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>User</returns>
        Task<User> Get(int userId);
        /// <summary>
        /// Gets users with pagination
        /// </summary>
        /// <param name="index">Amount to skip</param>
        /// <param name="amount">Amount to take</param>
        /// <returns>Users</returns>
        Task<IEnumerable<User>> Get(uint index, uint amount);
        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="value">New user model</param>
        /// <returns>Whether operation is successful</returns>
        Task<bool> Create(User value);
        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="value">User model to update</param>
        /// <returns>Whether operation is successful</returns>
        Task<bool> Modify(User value);
        /// <summary>
        /// Removes user with this id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Whether operation is successful</returns>
        Task<bool> Delete(int userId);
    }
}