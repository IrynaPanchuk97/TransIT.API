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
    /// Malfunction Group CRUD service
    /// </summary>
    /// <see cref="IMalfunctionGroupService"/>
    public class MalfunctionGroupService : CrudService<MalfunctionGroup>, IMalfunctionGroupService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public MalfunctionGroupService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<MalfunctionGroup>> logger,
            IMalfunctionGroupRepository repository) : base(unitOfWork, logger, repository) { }

        protected override Task<IEnumerable<MalfunctionGroup>> SearchExpressionAsync(IEnumerable<string> strs) =>
            _unitOfWork.MalfunctionGroupRepository.GetAllAsync(entity =>
                strs.Any(str => entity.Name.ToUpperInvariant().Contains(str)));
    }
}
