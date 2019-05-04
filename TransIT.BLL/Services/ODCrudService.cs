using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities.Abstractions;
using TransIT.DAL.Models.ViewModels;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services
{
    public class ODCrudService<TEntity> : IODCrudService<TEntity>
        where TEntity : class, IEntity, new()
    {
        protected readonly IODRepository<TEntity> _odRepository;

        public ODCrudService(IODRepository<TEntity> odRepository)
        {
            _odRepository = odRepository;
        }

        public virtual Task<IEnumerable<TEntity>> GetQueriedAsync() =>
            Task.FromResult(
                _odRepository.GetQueryable().AsEnumerable()
                );

        public virtual Task<IEnumerable<TEntity>> GetQueriedAsync(DataTableRequestViewModel dataFilter)
        {
            // TODO: Provide expression builder, which determines properties on runtime
            var exp = Expression.Empty();
            return Task.FromResult(
                _odRepository.GetQueryable()
                .Provider.CreateQuery<TEntity>(exp)
                .AsEnumerable());
        }
    }
}
