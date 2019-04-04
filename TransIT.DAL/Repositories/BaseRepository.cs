using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransIT.DAL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync<TEntity>();
        }

        public virtual async Task<EntityEntry<TEntity>> AddAsync(TEntity entity)
        {
            return await _context.Set<TEntity>().AddAsync(entity);
        }

        public virtual EntityEntry<TEntity> Remove(TEntity entity)
        {
            return _context.Set<TEntity>().Remove(entity);
        }

        public virtual EntityEntry<TEntity> Update(TEntity entity)
        {
            return _context.Set<TEntity>().Update(entity);
        }

        public virtual async Task<IEnumerable<TEntity>> GetRangeAsync(uint index, uint amount)
        {
           return await _context.Set<TEntity>().Skip((int)index).Take<TEntity>((int)amount).ToListAsync<TEntity>();
        }

    }
}
