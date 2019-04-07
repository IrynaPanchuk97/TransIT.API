using System.Collections.Generic;
using System.Threading.Tasks;

namespace TransIT.BLL.Services
{
    /// <summary>
    /// Set a behavior of services 
    /// </summary>
    public interface IDataService<T>
    {
        /// <summary>
        /// Gets entity by id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Entity</returns>
        Task<T> GetAsync(int id);
        /// <summary>
        /// Gets entities with pagination
        /// </summary>
        /// <param name="offset">Amount to skip</param>
        /// <param name="amount">Amount to take</param>
        /// <returns>Entities</returns>
        Task<IEnumerable<T>> GetAsync(uint offset, uint amount);
        /// <summary>
        /// Registers a new entity
        /// </summary>
        /// <param name="value">New entity</param>
        /// <returns>Created entity</returns>
        Task<T> CreateAsync(T value);
        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="value">Entity model to update</param>
        /// <returns>Updated entity</returns>
        Task<T> UpdateAsync(T value);
        /// <summary>
        /// Removes entity with this id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>void</returns>
        Task DeleteAsync(int id);
    }
}
