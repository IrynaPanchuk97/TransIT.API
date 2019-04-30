using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.BLL.Services
{
    public interface IODCrudService<TEntity> where TEntity : class, IEntity, new()
    {
        Task<IQueryable<TEntity>> GetQueriedAsync();
    }
}
