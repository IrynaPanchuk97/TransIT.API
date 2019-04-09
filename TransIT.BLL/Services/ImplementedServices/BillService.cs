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
    /// Bill CRUD service
    /// </summary>
    /// <see cref="IBillService"/>
    public class BillService : CrudService<Bill>, IBillService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public BillService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<Bill>> logger,
            IBillRepository repository) : base(unitOfWork, logger, repository) { }
            
        public override Task<IEnumerable<Bill>> SearchAsync(string search)
        {
            search = search.ToUpperInvariant();
            try
            {
                return _unitOfWork.BillRepository.GetAllAsync(a =>
                    a.Sum.ToString().Contains(search)
                    || search.Contains(a.Sum.ToString()));
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(SearchAsync));
                return null;
            }
        }
    }
}
