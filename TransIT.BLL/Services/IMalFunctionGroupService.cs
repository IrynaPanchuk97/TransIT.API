using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Services
{
    /// <summary>
    /// Service to manage Malfunction Group
    /// </summary>
    public interface IMalfunctionGroupService
    {
        /// <summary>
        /// Get Malfunction Group by id
        /// </summary>
        /// <param name="id">Malfunction Group id</param>
        /// <returns>Malfunction Group or null if not found</returns>
        Task<MalfunctionGroup> GetAsync(int id);

        /// <summary>
        /// Get Malfunction Groups with pagination
        /// </summary>
        /// <param name="offset">Number of groups to skip</param>
        /// <param name="size">Number of groups to take</param>
        /// <returns>Enumerable of Malfunction Groups</returns>
        Task<IEnumerable<MalfunctionGroup>> GetAllAsync(uint offset, uint size);

        /// <summary>
        /// Create new Malfunction Group
        /// </summary>
        /// <param name="malfunctionGroup">New Malfunction Group model</param>
        /// <returns>Created Malfunction Group or null in failure</returns>
        Task<MalfunctionGroup> CreateAsync(MalfunctionGroup malfunctionGroup);


        /// <summary>
        /// Update Malfunction Group
        /// </summary>
        /// <param name="malfunctionGroup">Malfunction Group model to update</param>
        /// <returns>Updated Malfunction Group model or null on failure</returns>
        Task<MalfunctionGroup> UpdateAsync(MalfunctionGroup malfunctionGroup);

        /// <summary>
        /// Deletes Malfunction Group with this id
        /// </summary>
        /// <param name="id">Id of Malfunction Group to delete</param>
        /// <returns>Always returns nothing</returns>
        Task DeleteAsync(int id);
    }
}
