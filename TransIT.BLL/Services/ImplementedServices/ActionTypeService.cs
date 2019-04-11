using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Malfunction Group CRUD service
    /// </summary>
    /// <see cref="IActionTypeService"/>
    public class ActionTypeService : CrudService<ActionType>, IActionTypeService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public ActionTypeService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<ActionType>> logger,
            IActionTypeRepository repository) : base(unitOfWork, logger, repository) { }
        
        protected override Task<IEnumerable<ActionType>> SearchExpressionAsync(IEnumerable<string> strs) =>
                _unitOfWork.ActionTypeRepository.GetAllAsync(entity =>
                    strs.Any(str => entity.Name.ToUpperInvariant().Contains(str)));
    }
}
