using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories
{
    class Repository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync<TEntity>();
        }

        public virtual async Task<EntityEntry<TEntity>> AddAsync(TEntity entity)
        {
            return await Context.Set<TEntity>().AddAsync(entity);
        }

        public virtual EntityEntry<TEntity> Remove(TEntity entity)
        {
            return Context.Set<TEntity>().Remove(entity);
        }

        public virtual EntityEntry<TEntity> Update(TEntity entity)
        {
            return Context.Set<TEntity>().Update(entity);
        }

        public virtual async Task<IEnumerable<TEntity>> GetRangeAsync(uint index, uint amount)
        {
           return await Context.Set<TEntity>().Skip((int)index).Take<TEntity>((int)amount).ToListAsync<TEntity>();
        }

    }
}
