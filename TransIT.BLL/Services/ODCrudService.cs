using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Query;
using Microsoft.OData;
using TransIT.DAL.Models.Entities.Abstractions;
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

        public virtual Task<IQueryable<TEntity>> GetQueriedAsync() =>
            Task.FromResult(_odRepository.GetQueryable());

        public virtual Task<IQueryable<TEntity>> GetQueriedAsync(ODataQueryOptions<TEntity> options)
        {
            try
            {
                return Task.FromResult(
                    (options ?? throw new ArgumentNullException())
                        .ApplyTo(_odRepository.GetQueryable())
                        .Cast<TEntity>());
            }
            catch (ODataException)
            {
                return null;
            }
        }
    }
}
