using TransIT.DAL.Models;
using TransIT.DAL.Repositories.Interfaces;

namespace TransIT.DAL.Repositories
{
    class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(TransITDBContext context)
            :base(context)
        {

        }
    }
}
