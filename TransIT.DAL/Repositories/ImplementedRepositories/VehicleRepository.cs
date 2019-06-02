using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(DbContext context)
            : base(context)
        {
        }

        public override Task<IQueryable<Vehicle>> SearchExpressionAsync(IEnumerable<string> strs) =>
            Task.FromResult(
                GetQueryable().Where(entity =>
                    strs.Any(str => entity.Brand.ToUpperInvariant().Contains(str)
                    || entity.RegNum.ToUpperInvariant().Contains(str)
                    || entity.InventoryId.ToUpperInvariant().Contains(str)
                    || entity.Model.ToUpperInvariant().Contains(str)
                    || entity.Vincode.ToUpperInvariant().Contains(str)
                    || entity.VehicleType.Name.ToUpperInvariant().Contains(str)
                    || entity.Location.Name.ToUpperInvariant().Contains(str)))
                );

        protected override IQueryable<Vehicle> ComplexEntities => Entities.
                    Include(u => u.VehicleType).
                    Include(u => u.Location).
                    Include(a => a.Create).
                    Include(b => b.Mod).OrderByDescending(u => u.ModDate).ThenByDescending(x => x.CreateDate);
    }
}
