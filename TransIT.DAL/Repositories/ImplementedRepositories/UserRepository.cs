﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context)
        {
        }
        
        public override Task<IQueryable<User>> SearchExpressionAsync(IEnumerable<string> strs) =>
            Task.FromResult(
                GetQueryable().Where(entity =>
                    strs.Any(str => entity.Login.ToUpperInvariant().Contains(str)
                    || entity.Email.ToUpperInvariant().Contains(str)
                    || entity.PhoneNumber.ToUpperInvariant().Contains(str)
                    || entity.LastName.ToUpperInvariant().Contains(str)
                    || entity.FirstName.ToUpperInvariant().Contains(str)))
                );

        protected override IQueryable<User> ComplexEntities => Entities.
            Include(u => u.Role).
            Include(a => a.Create).
            Include(b => b.Mod).OrderByDescending(u => u.ModDate).ThenByDescending(x => x.CreateDate);
    }
}
