using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Query;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.BLL.Services
{
    public interface IODCrudService<TEntity> where TEntity : class, IEntity, new()
    {
        Task<IQueryable<TEntity>> GetQueriedAsync();
        Task<IQueryable<TEntity>> GetQueriedAsync(ODataQueryOptions<TEntity> options);
    }
}
