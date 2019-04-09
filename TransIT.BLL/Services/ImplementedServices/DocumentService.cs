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
        
        public override Task<IEnumerable<Document>> SearchAsync(string search)
        {
            search = search.ToUpperInvariant();
            return _unitOfWork.DocumentRepository.GetAllAsync(a =>
                a.Name.ToUpperInvariant().Contains(search)
                || a.Description.ToUpperInvariant().Contains(search)
                || search.Contains(a.Name.ToUpperInvariant())
                || search.Contains(a.Description.ToUpperInvariant()));
        }
    }
}
