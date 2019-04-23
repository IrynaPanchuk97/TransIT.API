using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TransIT.BLL.Services.InterfacesRepositories;
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
            IIssueLogRepository repository
        ) : base(unitOfWork, logger, repository)
        {
            _issueRepository = issueRepository;
        }

        public async Task<IEnumerable<IssueLog>> GetRangeByIssueIdAsync(int issueId)
        {
            try
            {
                return await _repository.GetAllAsync(i => i.IssueId == issueId);
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, nameof(GetRangeByIssueIdAsync), e.Entries);
                return null;
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
                var assignedUserId = model.Issue.AssignedTo;
                model.Issue = await _issueRepository.GetByIdAsync((int)model.IssueId);
                model.OldStateId = model.Issue.StateId;
                model.Issue.StateId = model.NewStateId;
                model.Issue.AssignedTo = assignedUserId;
                model.Issue.State = null;
                return await base.CreateAsync(model);
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, nameof(CreateAsync), e.Entries);
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(CreateAsync));
                throw e;
            }
        }
        
        protected override Task<IEnumerable<IssueLog>> SearchExpressionAsync(IEnumerable<string> strs) =>
            _unitOfWork.IssueLogRepository.GetAllAsync(entity =>
                strs.Any(str => entity.Description.ToUpperInvariant().Contains(str)));
    }
}
