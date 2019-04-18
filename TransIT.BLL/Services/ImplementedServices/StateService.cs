using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// State Group CRUD service
    /// </summary>
    /// <see cref="IStateService"/>
    public class StateService : CrudService<State>, IStateService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public StateService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<State>> logger,
            IStateRepository repository) : base(unitOfWork, logger, repository) { }

        /// <summary>
        /// Returns state by name
        /// </summary>
        /// <param name="name">State's name</param>
        /// <returns>State</returns>
        /// <see cref="IStateService"/>
        public async Task<State> GetStateByNameAsync(string name)
        {
            var states = await _repository.GetAllAsync(s => s.Name == name);
            return states.SingleOrDefault();
        }

        protected override Task<IEnumerable<State>> SearchExpressionAsync(IEnumerable<string> strs) =>
            _unitOfWork.StateRepository.GetAllAsync(entity =>
                strs.Any(str => entity.Name.ToUpperInvariant().Contains(str)));
    }
}
