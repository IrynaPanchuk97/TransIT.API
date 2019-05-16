using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Query;
using Microsoft.OData;
using TransIT.BLL.Helpers;
using TransIT.DAL.Models.Entities.Abstractions;
using TransIT.DAL.Models.ViewModels;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services
{
    public class FilterService<TEntity> : IODCrudService<TEntity>
        where TEntity : class, IEntity, new()
    {        
        protected readonly IQueryRepository<TEntity> _queryRepository;
        protected readonly ICrudService<TEntity> _crudService;

        public ulong TotalRecordsAmount => (ulong)_queryRepository.GetQueryable().LongCount();
        
        public FilterService(
            IQueryRepository<TEntity> queryRepository,
            ICrudService<TEntity> crudService)
        {
            _queryRepository = queryRepository;
            _crudService = crudService;
        }

        public virtual Task<IEnumerable<TEntity>> GetQueriedAsync() =>
            Task.FromResult<IEnumerable<TEntity>>(
                _queryRepository.GetQueryable()
                );

        public virtual async Task<IEnumerable<TEntity>> GetQueriedAsync(DataTableRequestViewModel dataFilter) => 
            await GetQueriedAsync(dataFilter, await DetermineDataSource(dataFilter));

        public virtual async Task<IEnumerable<TEntity>> GetQueriedWithWhereAsync(
            DataTableRequestViewModel dataFilter,
            Expression<Func<TEntity, bool>> whereExpression)
        {
            _ = ThrowIfIncorrectArguments(dataFilter);
            return ProcessQuery(
                dataFilter,
                await DetermineDataSource(dataFilter),
                whereExpression
                );
        }

        protected virtual Task<IQueryable<TEntity>> GetQueriedAsync(
            DataTableRequestViewModel dataFilter,
            IQueryable<TEntity> dataSource)
        {
            _ = ThrowIfIncorrectArguments(dataFilter);
            return Task.FromResult(ProcessQuery(dataFilter, dataSource));
        }
        
        private bool ThrowIfIncorrectArguments(DataTableRequestViewModel dataFilter) =>
            !dataFilter.Columns.Any()
                ? throw new ArgumentException(
                    $"{nameof(DataTableRequestViewModel)}.{nameof(dataFilter.Columns)} is empty.")
                : !dataFilter.Order.Any()
                    ? throw new ArgumentException(
                        $"{nameof(DataTableRequestViewModel)}.{nameof(dataFilter.Order)} is empty.")
                    : true;
        
        public virtual Task<IEnumerable<TEntity>> GetQueriedAsync(ODataQueryOptions<TEntity> options)
        {
            try
            {
                return Task.FromResult<IEnumerable<TEntity>>(
                    options
                        .ApplyTo(_queryRepository.GetQueryable())
                        .Cast<TEntity>()
                    );
            }
            catch (ODataException)
            {
                return Task.FromResult<IEnumerable<TEntity>>(null);
            }
        }

        private async Task<IQueryable<TEntity>> DetermineDataSource(DataTableRequestViewModel dataFilter) =>
            string.IsNullOrEmpty(dataFilter.Search.Value)
                ? _queryRepository.GetQueryable()
                : (await _crudService.SearchAsync(dataFilter.Search.Value)).AsQueryable();

        private IQueryable<TEntity> ProcessQuery(DataTableRequestViewModel dataFilter, IQueryable<TEntity> data) =>
            TableOrderBy(
                    dataFilter,
                    dataFilter.Filters == null || dataFilter.Length == 0
                        ? data
                        : ProcessQueryFilter(dataFilter.Filters, data)
                    )
                .Skip(dataFilter.Start)
                .Take(dataFilter.Length);

        private IQueryable<TEntity> ProcessQueryFilter(
            IEnumerable<DataTableRequestViewModel.FilterType> filters,
            IQueryable<TEntity> data)
        {
            filters.ToList().ForEach(filter =>
                data = TableWhereEqual(filter, data)
                );
            return data;
        }

        private IQueryable<TEntity> ProcessQuery(
            DataTableRequestViewModel dataFilter,
            IQueryable<TEntity> data,
            Expression<Func<TEntity, bool>> whereExpression) =>
            ProcessQuery(dataFilter, data)
                .Where(whereExpression);

        private IQueryable<TEntity> TableOrderBy(DataTableRequestViewModel dataFilter, IQueryable<TEntity> data)
        {
            if (dataFilter.Order == null
                || dataFilter.Columns == null
                || !dataFilter.Order.Any()
                || !dataFilter.Columns.Any()) return data;
            
            data = data.OrderBy(
                dataFilter.Columns[dataFilter.Order[0].Column].Data,
                dataFilter.Order[0].Dir == DataTableRequestViewModel.DataTableDescending
                );
            for (var i = 1; i < dataFilter.Order.Length; ++i)
                data = data.ThenBy(
                    dataFilter.Columns[dataFilter.Order[i].Column].Data,
                    dataFilter.Order[i].Dir == DataTableRequestViewModel.DataTableDescending
                    );
            return data;
        }

        private IQueryable<TEntity> TableWhereEqual(DataTableRequestViewModel.FilterType filter, IQueryable<TEntity> data)
        {
            var value = FilterProcessingHelper.DetectStringType(filter.Value);
            return value == null
                ? data
                : data.Where(filter.EntityPropertyPath, value, filter.Operator);
        }
    }
}
