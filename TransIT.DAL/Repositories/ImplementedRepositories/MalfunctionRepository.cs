using Microsoft.EntityFrameworkCore;
using System.Linq;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class MalfunctionRepository : BaseRepository<Malfunction>, IMalfunctionRepository
    {
        public MalfunctionRepository(DbContext context)
               : base(context)
        {
        }

        protected override IQueryable<Malfunction> ComplexEntities => Entities.
                   Include(t => t.Create).
                   Include(z => z.Mod);
    }
}
