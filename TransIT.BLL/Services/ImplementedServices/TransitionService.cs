using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public async override Task DeleteAsync(int id)
        {
            try
            {
                var model = await GetAsync(id);
                if (model.IsFixed)
                {
                    throw new ConstraintException("Current state can not be deleted");
                }

                _repository.Remove(model);
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateException e)
            {
                var sqlExc = e.GetBaseException() as SqlException;
                if (sqlExc?.Number == 547)
                {
                    _logger.LogDebug(sqlExc, $"Number of sql exception: {sqlExc.Number.ToString()}");
                    throw new ConstraintException("There are constrained entities, delete them firstly.", sqlExc);
                }
                _logger.LogError(e, nameof(DeleteAsync), e.Entries);
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(DeleteAsync));
                throw;
            }
        }
    }
}
