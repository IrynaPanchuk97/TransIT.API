using TransIT.BLL.Services.ImplementedServices;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.BLL.Tests.Services.ImplementedServices
{
    public class MalfunctionSubgroupServiceTests : CrudServiceTest<MalfunctionSubgroup>
    {
        protected override void InitializeService()
        {
            var mock = _repository.As<IMalfunctionSubgroupRepository>();
            _crudService = new MalfunctionSubgroupService(_unitOfWork.Object, _logger.Object, mock.Object);
        }
    }
}
