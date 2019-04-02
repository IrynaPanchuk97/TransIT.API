using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransIT.DAL.Repositories.Interfaces
{
    interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        EntityEntry<TEntity> Add(TEntity entity);
        EntityEntry<TEntity> Remove(TEntity entity);
    }
}
