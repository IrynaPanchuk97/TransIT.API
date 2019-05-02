using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Query;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.BLL.Services
{
    public interface IODCrudService<TEntity> where TEntity : class, IEntity, new()
    {
        Task<IEnumerable<TEntity>> GetQueriedAsync();
        Task<IEnumerable<TEntity>> GetQueriedAsync(ODataQueryOptions<TEntity> options);
    }
}
