using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DBContext context)
            : base(context)
        {
            _ = context.Set<User>();
        }
    }
}
