using System;
using System.Collections.Generic;
using System.Text;
using TransIT.DAL.Models;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(DBContext context)
               : base(context)
        {
            _ = context.Set<Role>();
        }
    }
}
