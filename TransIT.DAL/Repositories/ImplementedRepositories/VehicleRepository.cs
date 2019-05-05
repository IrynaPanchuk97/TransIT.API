using Microsoft.EntityFrameworkCore;
using System.Linq;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(DbContext context)
            :base(context)
        {
        }

        protected override IQueryable<Vehicle> ComplexEntities => Entities.
                    Include(u => u.VehicleType).
                    Include(a => a.Create).
                    Include(b => b.Mod).OrderByDescending(u => u.ModDate).OrderByDescending(x => x.CreateDate);
    }
}
