using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class VehicleTypeRepository : BaseRepository<VehicleType>, IVehicleTypeRepository
    {
        public VehicleTypeRepository(DbContext context)
            : base(context)
        {
        }
    }
}
