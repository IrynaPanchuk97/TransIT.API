using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TransIT.DAL.Models.Entities.Abstractions;
using TransIT.DAL.Repositories;
using TransIT.DAL.UnitOfWork;

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
    }
}
