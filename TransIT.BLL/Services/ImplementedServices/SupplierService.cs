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

        protected override Task<IEnumerable<Supplier>> SearchExpressionAsync(IEnumerable<string> strs) =>
            _unitOfWork.SupplierRepository.GetAllAsync(entity =>
                strs.Any(str =>entity.Name.ToUpperInvariant().Contains(str)
                || entity.Edrpou.ToUpperInvariant().Contains(str)
                || entity.Country.Name.ToUpperInvariant().Contains(str)
                || entity.Currency.ShortName.ToUpperInvariant().Contains(str)
                || entity.Currency.FullName.ToUpperInvariant().Contains(str)));
    }
}
