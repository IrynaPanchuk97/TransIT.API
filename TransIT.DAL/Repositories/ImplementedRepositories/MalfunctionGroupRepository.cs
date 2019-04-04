using System;
using System.Collections.Generic;
using System.Text;
using TransIT.DAL.Models;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class MalfunctionGroupRepository : BaseRepository<MalfunctionGroup>, IMalfunctionGroupRepository
    {
        public MalfunctionGroupRepository(DBContext context)
               : base(context)
        {
            _ = context.Set<MalfunctionGroup>();
        }
    }
}
