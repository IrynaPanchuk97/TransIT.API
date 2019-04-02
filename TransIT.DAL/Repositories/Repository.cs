using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.DAL.Repositories.Interfaces;

namespace TransIT.DAL.Repositories
{
    class Repository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync<TEntity>();
        }

        public async Task<EntityEntry<TEntity>> AddAsync(TEntity entity)
        {
            return await Context.Set<TEntity>().AddAsync(entity);
        }

        public EntityEntry<TEntity> Remove(TEntity entity)
        {
            return Context.Set<TEntity>().Remove(entity);
        }

    }
}
