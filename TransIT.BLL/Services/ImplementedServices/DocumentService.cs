using System;
using System.Collections.Generic;
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
    /// Document CRUD service
    /// </summary>
    /// <see cref="IDocumentService"/>
    public class DocumentService : CrudService<Document>, IDocumentService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public DocumentService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<Document>> logger,
            IDocumentRepository repository) : base(unitOfWork, logger, repository) { }

        public Task<IEnumerable<Document>> GetRangeByIssueLogIdAsync(int issueLogId) =>
            _repository.GetAllAsync(i => i.IssueLogId == issueLogId);

        protected override Task<IEnumerable<Document>> SearchExpressionAsync(IEnumerable<string> strs) =>
            _unitOfWork.DocumentRepository.GetAllAsync(entity =>
                strs.Any(str => entity.Name.ToUpperInvariant().Contains(str)
                    || entity.Description.ToUpperInvariant().Contains(str)));
    }
}
