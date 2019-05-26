using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// IssueLog CRUD service
    /// </summary>
    /// <see cref="IIssueLogService"/>
    public class IssueLogService : CrudService<IssueLog>, IIssueLogService
    {
        private IIssueRepository _issueRepository;
        private ITransitionRepository _transitionRepository;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public IssueLogService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<IssueLog>> logger,
            IIssueRepository issueRepository,
            IIssueLogRepository repository,
            ITransitionRepository transitionRepository
        ) : base(unitOfWork, logger, repository)
        {
            _issueRepository = issueRepository;
            _transitionRepository = transitionRepository;
        }

        public async Task<IEnumerable<IssueLog>> GetRangeByIssueIdAsync(int issueId)
        {
            try
            {
                return await _repository.GetAllAsync(i => i.IssueId == issueId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(GetRangeByIssueIdAsync));
                throw e;
            }
        }
        
        public override async Task<IssueLog> CreateAsync(IssueLog model)
        {
            try
            {
                var oldIssue = model.Issue;
                model.Issue = await _issueRepository.GetByIdAsync((int)model.IssueId);
                model.OldStateId = model.Issue.StateId;
                model.Issue.StateId = model.NewStateId;
                model.Issue.Deadline = oldIssue.Deadline;
                model.Issue.AssignedToId = oldIssue.AssignedToId;
                
//                if (model.OldStateId != model.NewStateId
//                    && model.OldStateId != null
//                    && model.NewStateId != null
//                    && model.ActionTypeId != null
//                    && !(await _transitionRepository.GetAllAsync(x =>
//                        x.FromStateId == model.OldStateId
//                        && x.ToStateId == model.NewStateId
//                        && x.ActionTypeId == model.ActionTypeId)
//                    ).Any())
//                    throw new ConstraintException("Can not move to the state according to transition settings.");
                
                return await base.CreateAsync(model);
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(CreateAsync));
                throw e;
            }
        }
        
        protected override Task<IEnumerable<IssueLog>> SearchExpressionAsync(IEnumerable<string> strs) =>
            _unitOfWork.IssueLogRepository.GetAllAsync(entity =>
                strs.Any(str => entity.Description.ToUpperInvariant().Contains(str)
                || entity.NewState.Name.ToUpperInvariant().Contains(str)
                || entity.OldState.Name.ToUpperInvariant().Contains(str)));
    }
}
