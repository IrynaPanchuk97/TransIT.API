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

        protected override IQueryable<Malfunction> ComplexEntities => Entities
                    .Include(m => m.Create)
                    .Include(m => m.Mod)
                    .Include(m => m.MalfunctionSubgroup)
                        .ThenInclude(s => s.MalfunctionGroup).OrderByDescending(u => u.ModDate).OrderByDescending(x => x.CreateDate);
    }
}
