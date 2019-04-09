using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            IIssueLogRepository repository) : base(unitOfWork, logger, repository) { }
        
        public override Task<IEnumerable<IssueLog>> SearchAsync(string search)
        {
            search = search.ToUpperInvariant();
            try
            {
                return _unitOfWork.IssueLogRepository.GetAllAsync(a =>
                    a.Description.ToUpperInvariant().Contains(search)
                    || search.Contains(a.Description.ToUpperInvariant()));
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(SearchAsync));
                return null;
            }
        }
    }
}
