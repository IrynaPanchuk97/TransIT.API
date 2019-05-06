using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Services.InterfacesRepositories
{
    /// <summary>
    /// User model CRUD
    /// </summary>
    public interface IUserService : ICrudService<User>
    {
        Task<User> UpdateAsync(User model);
        Task<IEnumerable<User>> GetAssignees(uint offset, uint amount);
    }
}
