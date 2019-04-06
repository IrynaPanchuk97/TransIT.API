using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TransIT.DAL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        protected DbSet<TEntity> _entities;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public virtual Task<TEntity> GetByIdAsync(int id)
        {
            return Entities.FindAsync(id);
        }

        public virtual Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.SingleOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Entities.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Entities.Where(predicate).ToListAsync();
        }

        public virtual Task<EntityEntry<TEntity>> AddAsync(TEntity entity)
        {
            return Entities.AddAsync(entity);
        }

        public virtual EntityEntry<TEntity> Remove(TEntity entity)
        {
            return Entities.Remove(entity);
        }

        public virtual EntityEntry<TEntity> Update(TEntity entity)
        {
            return Entities.Update(entity);
        }

        public virtual async Task<IEnumerable<TEntity>> GetRangeAsync(uint index, uint amount)
        {
           return await Entities.Skip((int)index).Take((int)amount).ToListAsync();
        }

        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<TEntity>();

                return _entities;
            }
        }

    }
}
