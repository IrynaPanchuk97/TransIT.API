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
    /// Supplier CRUD service
    /// </summary>
    /// <see cref="ISupplierService"/>
    public class SupplierService : CrudService<Supplier>, ISupplierService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public SupplierService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<Supplier>> logger,
            ISupplierRepository repository) : base(unitOfWork, logger, repository) { }
        
        public override Task<IEnumerable<Supplier>> SearchAsync(string search)
        {
            search = search.ToUpperInvariant();
            try
            {
                return _unitOfWork.SupplierRepository.GetAllAsync(a =>
                    a.Name.ToUpperInvariant().Contains(search)
                    || search.Contains(a.Name.ToUpperInvariant()));
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(SearchAsync));
                return null;
            }
        }
    }
}
