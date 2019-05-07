using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Models.ViewModels;

namespace TransIT.BLL.Services.InterfacesRepositories
{
    /// <summary>
    /// User model CRUD
    /// </summary>
    public interface IUserService : ICrudService<User>
    {
        Task<User> UpdatePasswordAsync(int id, ChangePasswordViewModel changePassword);
        Task<User> UpdateAsync(User model);
        Task<IEnumerable<User>> GetAssignees(uint offset, uint amount);
    }
}
