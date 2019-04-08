using Microsoft.EntityFrameworkCore;
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

        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<TEntity>>(Entities.AsQueryable());
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult<IEnumerable<TEntity>>(Entities.Where(predicate).AsQueryable());
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            return (await Entities.AddAsync(entity)).Entity;
        }

        public virtual TEntity Remove(TEntity entity)
        {
            return  Entities.Remove(entity).Entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            return  Entities.Update(entity).Entity;
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
