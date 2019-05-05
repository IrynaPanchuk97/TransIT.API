using System.Threading.Tasks;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;
using TransIT.DAL.Models.ViewModels;

namespace TransIT.BLL.Services
{
    public interface IFilterService<TEntity> where TEntity : class, IEntity, new()
    {
        ulong TotalRecordsAmount { get; }
        Task<IEnumerable<TEntity>> GetQueriedAsync();
        Task<IEnumerable<TEntity>> GetQueriedAsync(DataTableRequestViewModel dataFilter);
    }
}
