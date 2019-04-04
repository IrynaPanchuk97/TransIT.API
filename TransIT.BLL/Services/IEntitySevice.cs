using System;
using System.Collections.Generic;
using System.Text;
using TransIT.DAL.Models;
using TransIT.DAL;
using TransIT.DAL.Repositories;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TransIT.BLL.Services
{
    interface IEntityService<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetRangeAsync(uint index, uint amount);
        Task<EntityEntry<TEntity>> AddAsync(TEntity entity);
        EntityEntry<TEntity> Remove(TEntity entity);
        EntityEntry<TEntity> Update(TEntity entity);
    }
}
