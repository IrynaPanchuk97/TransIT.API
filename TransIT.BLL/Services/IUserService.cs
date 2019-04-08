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
        Task<User> GetAsync(int userId);
        /// <summary>
        /// Gets users with pagination
        /// </summary>
        /// <param name="offset">Amount to skip</param>
        /// <param name="amount">Amount to take</param>
        /// <returns>Users</returns>
        Task<IEnumerable<User>> GetAsync(uint offset, uint amount);
        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="user">New user model</param>
        /// <returns>Whether operation is successful</returns>
        Task<User> CreateAsync(User user);
        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="user">User model to update</param>
        /// <returns>Whether operation is successful</returns>
        Task<User> UpdateAsync(User user);
        /// <summary>
        /// Removes user with this id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Whether operation is successful</returns>
        Task DeleteAsync(int userId);
    }
}
