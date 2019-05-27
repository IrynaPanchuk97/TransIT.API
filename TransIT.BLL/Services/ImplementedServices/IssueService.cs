using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Issue CRUD service
    /// </summary>
    /// <see cref="IIssueService"/>
    public class IssueService : CrudService<Issue>, IIssueService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public IssueService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<Issue>> logger,
            IIssueRepository repository) : base(unitOfWork, logger, repository) { }

        /// <see cref="IIssueService"/>
        public async Task<IEnumerable<Issue>> GetRegisteredIssuesAsync(uint offset, uint amount, int userId)
        {
            var issues = await _repository.GetAllAsync(i => i.CreateId == userId);
            return issues.AsQueryable().Skip((int)offset).Take((int)amount);
        }

        public async Task DeleteByUserAsync(int issueId, int userId)
        {
            try
            {
                var issueToDelete = await GetAsync(issueId);
                if (issueToDelete?.CreateId != userId)
                    throw new UnauthorizedAccessException("Current user doesn't have access to delete this issue");

                _repository.Remove(issueToDelete);
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
