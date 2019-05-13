using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNet.OData.Query;
using TransIT.DAL.Models.Entities.Abstractions;
using TransIT.DAL.Models.ViewModels;

namespace TransIT.BLL.Services
{
    public interface IODCrudService<TEntity> : IFilterService<TEntity> where TEntity : class, IEntity, new()
    {
        Task<IEnumerable<TEntity>> GetQueriedAsync(ODataQueryOptions<TEntity> options);
    }
    
    public interface IFilterService<TEntity> where TEntity : class, IEntity, new()
    {
        ulong TotalRecordsAmount { get; }
        Task<IEnumerable<TEntity>> GetQueriedAsync();
        Task<IEnumerable<TEntity>> GetQueriedAsync(DataTableRequestViewModel dataFilter);
        Task<IEnumerable<TEntity>> GetQueriedWithWhereAsync(
            DataTableRequestViewModel dataFilter,
            Expression<Func<TEntity, bool>> matchExpression);
    }
}
