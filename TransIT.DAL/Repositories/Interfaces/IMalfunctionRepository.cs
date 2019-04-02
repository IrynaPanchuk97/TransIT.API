using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TransIT.DAL.Models;

namespace TransIT.DAL.Repositories.Interfaces
{
    class IMalfunctionRepository : IBaseRepository<Malfunction>
    {
        public EntityEntry<Malfunction> Add(Malfunction entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Malfunction> GetAll()
        {
            throw new NotImplementedException();
        }

        public Malfunction GetById(int id)
        {
            throw new NotImplementedException();
        }

        public EntityEntry<Malfunction> Remove(Malfunction entity)
        {
            throw new NotImplementedException();
        }
    }
}
