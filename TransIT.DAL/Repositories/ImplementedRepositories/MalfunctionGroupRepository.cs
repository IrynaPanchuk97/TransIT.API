using Microsoft.EntityFrameworkCore;
using System.Linq;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class MalfunctionGroupRepository : BaseRepository<MalfunctionGroup>, IMalfunctionGroupRepository
    {
        public MalfunctionGroupRepository(DbContext context)
               : base(context)
        {
        }

        protected override IQueryable<MalfunctionGroup> ComplexEntities => Entities.
                   Include(t => t.Create).
                   Include(z => z.Mod);
                   
    }
}
