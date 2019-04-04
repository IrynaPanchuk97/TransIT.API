using System;
using System.Collections.Generic;
using System.Text;
using TransIT.DAL.Models;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    class MalfunctionSubgroupRepository: BaseRepository<MalfunctionSubgroup>, IMalfunctionSybgroupRepository
    {
        public MalfunctionSubgroupRepository(DBContext context)
            : base(context)
        {
            _ = context.Set<MalfunctionSubgroup>();
        }
    }
}
