using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Malfunction Subgroup CRUD service
    /// </summary>
    /// <see cref="IMalfunctionSubgroupService"/>
    public class MalfunctionSubgroupService : CrudService<MalfunctionSubgroup>, IMalfunctionSubgroupService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public MalfunctionSubgroupService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<MalfunctionSubgroup>> logger,
            IMalfunctionSubgroupRepository repository) : base(unitOfWork, logger, repository) { }

        protected override Task<IEnumerable<MalfunctionSubgroup>> SearchExpressionAsync(IEnumerable<string> strs) =>
            _unitOfWork.MalfunctionSubgroupRepository.GetAllAsync(entity =>
                strs.Any(str => entity.Name.ToUpperInvariant().Contains(str)));
    }
}
