using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    public class TransitionService : CrudService<Transition>, ITransitionService
    {
        public TransitionService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<Transition>> logger,
            ITransitionRepository repository) : base(unitOfWork, logger, repository) { }

        protected override Task<IEnumerable<Transition>> SearchExpressionAsync(IEnumerable<string> strs) =>
            _unitOfWork
            .TransitionRepository
            .GetAllAsync(transition =>
                strs.Any(str =>
                    transition.FromState.TransName.ToUpperInvariant().Contains(str)
                    || transition.ToState.TransName.ToUpperInvariant().Contains(str)
                    || transition.ActionType.Name.ToUpperInvariant().Contains(str)
                )
            );
    }
}
