using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNet.OData.Query;
using TransIT.DAL.Models.Entities.Abstractions;
using TransIT.DAL.Models.ViewModels;

namespace TransIT.BLL.Services
{    
    public interface IFilterService<TEntity> where TEntity : class, IEntity, new()
    {
        ulong TotalRecordsAmount();
        ulong TotalRecordsAmount(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> GetQueriedAsync();
        Task<IEnumerable<TEntity>> GetQueriedAsync(DataTableRequestViewModel dataFilter);
        Task<IEnumerable<TEntity>> GetQueriedWithWhereAsync(
            DataTableRequestViewModel dataFilter,
            Expression<Func<TEntity, bool>> matchExpression);
    }
}
