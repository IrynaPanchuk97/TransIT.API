using System;
using System.Collections.Generic;
using System.Text;
using TransIT.DAL.Models;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    class MalfunctionRepository: BaseRepository<Malfunction>, IMalfunctionRepository
    {
        public MalfunctionRepository(DBContext context)
               : base(context)
        {
            _ = context.Set<Vehicle>();
        }
    }
}
