﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext _context;
        protected DbSet<TEntity> _entities;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public virtual Task<TEntity> GetByIdAsync(int id)
        {
            return ComplexEntities.SingleOrDefaultAsync(t => t.Id == id);
        }

        public virtual Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return ComplexEntities.SingleOrDefaultAsync(predicate);
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<TEntity>>(ComplexEntities);
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult<IEnumerable<TEntity>>(ComplexEntities.Where(predicate));
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
           return await ComplexEntities.Skip((int)index).Take((int)amount).ToListAsync();
        }

        public virtual TEntity UpdateWithIgnoreProperty<TProperty>(
            TEntity entity, Expression<Func<TEntity, TProperty>> ignorePropertyExpression)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(ignorePropertyExpression).IsModified = false;
            return entity;
        }
        
        protected virtual DbSet<TEntity> Entities => _entities ?? (_entities = _context.Set<TEntity>());

        protected virtual IQueryable<TEntity> ComplexEntities => Entities;

    }
}
