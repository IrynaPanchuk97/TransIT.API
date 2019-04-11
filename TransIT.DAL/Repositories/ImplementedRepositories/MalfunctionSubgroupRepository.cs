using Microsoft.EntityFrameworkCore;
using System.Linq;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class MalfunctionSubgroupRepository : BaseRepository<MalfunctionSubgroup>, IMalfunctionSubgroupRepository
    {
        public MalfunctionSubgroupRepository(DbContext context)
            : base(context)
        {
        }

        protected override IQueryable<MalfunctionSubgroup> ComplexEntities => Entities.
                   Include(t => t.Create).
                   Include(z => z.Mod).
                   Include(a => a.MalfunctionGroup);
    }
}
