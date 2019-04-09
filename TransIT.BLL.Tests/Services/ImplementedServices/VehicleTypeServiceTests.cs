using TransIT.BLL.Services;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.BLL.Tests.Services.ImplementedServices
{
    public class VehicleTypeServiceTests : CrudServiceTest<VehicleType>
    {
        protected override void InitializeService()
        {
            var mock = _repository.As<IVehicleTypeRepository>();
            _crudService = new VehicleTypeService(_unitOfWork.Object, _logger.Object, mock.Object);
        }
    }
}
