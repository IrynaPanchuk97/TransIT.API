using TransIT.DAL.Models;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(TransITDBContext context)
            :base(context)
        {

        }
    }
}
