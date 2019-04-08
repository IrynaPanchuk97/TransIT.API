using System;
using System.Collections.Generic;
using System.Text;
using TransIT.BLL.Security.Hashers;
using TransIT.BLL.Services.ImplementedServices;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.BLL.Tests.Services.ImplementedServices
{
    public class UserServiceTests : CrudServiceTest<User>
    {
        protected override void InitializeService()
        {
            var mock = _repository.As<IUserRepository>();
            _crudService = new UserService(_unitOfWork.Object, _logger.Object, mock.Object, new PasswordHasher());
        }
    }
}
