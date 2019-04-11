using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Malfunction CRUD service
    /// </summary>
    /// <see cref="IMalfunctionService"/>
    public class RoleService : CrudService<Role>, IRoleService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public RoleService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<Role>> logger,
            IRoleRepository repository) : base(unitOfWork, logger, repository) { }

        protected override Task<IEnumerable<Role>> SearchExpressionAsync(IEnumerable<string> strs) =>
            _unitOfWork.RoleRepository.GetAllAsync(entity =>
                strs.Any(str => entity.Name.ToUpperInvariant().Contains(str)));
    }
}
