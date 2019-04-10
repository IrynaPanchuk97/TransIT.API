using System;
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

        protected override Task<IEnumerable<Issue>> SearchExpressionAsync(IEnumerable<string> strs) =>
            _unitOfWork.IssueRepository.GetAllAsync(entity =>
                strs.Any(str => entity.Summary.ToUpperInvariant().Contains(str)));
    }
}
