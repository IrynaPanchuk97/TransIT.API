using System.Linq;
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

        protected override IQueryable<User> ComplexEntities => Entities.
            Include(u => u.Role).
            Include(a => a.Create).
            Include(b => b.Mod).OrderByDescending(u => u.ModDate).OrderByDescending(x => x.CreateDate);
    }
}
