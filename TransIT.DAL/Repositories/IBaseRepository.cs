using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TransIT.DAL.Repositories
{
    interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetRangeAsync(uint index, uint amount);
        Task<EntityEntry<TEntity>> AddAsync(TEntity entity);
        EntityEntry<TEntity> Remove(TEntity entity);
        EntityEntry<TEntity> Update(TEntity entity);
    }
}
