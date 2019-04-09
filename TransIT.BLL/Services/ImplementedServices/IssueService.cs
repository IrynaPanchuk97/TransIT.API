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
        
        public override Task<IEnumerable<Issue>> SearchAsync(string search)
        {
            search = search.ToUpperInvariant();
            return _unitOfWork.IssueRepository.GetAllAsync(a =>
                a.Summary.ToUpperInvariant().Contains(search)
                || search.Contains(a.Summary.ToUpperInvariant()));
        }
    }
}
