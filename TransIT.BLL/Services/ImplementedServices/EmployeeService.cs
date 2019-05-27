using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Employee CRUD service
    /// </summary>
    /// <see cref="IEmployeeService"/>
    public class EmployeeService : CrudService<Employee>, IEmployeeService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public EmployeeService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<Employee>> logger,
            IEmployeeRepository repository) : base(unitOfWork, logger, repository) { }

        protected override Task<IEnumerable<Employee>> SearchExpressionAsync(IEnumerable<string> strs) =>
            _unitOfWork.EmployeeRepository.GetAllAsync(entity =>
                strs.Any(str => entity.Post.Name.ToUpperInvariant().Contains(str)
                || !string.IsNullOrEmpty(entity.FirstName) && entity.FirstName.ToUpperInvariant().Contains(str)
                || !string.IsNullOrEmpty(entity.MiddleName) && entity.MiddleName.ToUpperInvariant().Contains(str)
                || !string.IsNullOrEmpty(entity.LastName) && entity.LastName.ToUpperInvariant().Contains(str)
                || entity.BoardNumber.ToString().ToUpperInvariant().Contains(str)
                || entity.ShortName.ToUpperInvariant().Contains(str)));
    }
}
