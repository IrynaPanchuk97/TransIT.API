using TransIT.BLL.Services.ImplementedServices;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.BLL.Tests.Services.ImplementedServices
{
    public class ActionTypeServiceTests : CrudServiceTest<ActionType>
    {
        protected override void InitializeService()
        {
            var mock = _repository.As<IActionTypeRepository>();
            _crudService = new ActionTypeService(_unitOfWork.Object, _logger.Object, mock.Object);
        }
    }
}
