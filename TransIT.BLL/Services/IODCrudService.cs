using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Query;
using TransIT.DAL.Models.Entities.Abstractions;
using TransIT.DAL.Models.ViewModels;

namespace TransIT.BLL.Services
{
    public interface IODCrudService<TEntity> where TEntity : class, IEntity, new()
    {
        Task<IEnumerable<TEntity>> GetQueriedAsync();
        Task<IEnumerable<TEntity>> GetQueriedAsync(DataTableRequestViewModel dataFilter);
    }
}
