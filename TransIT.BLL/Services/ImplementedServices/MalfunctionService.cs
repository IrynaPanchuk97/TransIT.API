using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
    public class MalfunctionService : CrudService<Malfunction>,IMalfunctionService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on entity</param>
        /// <see cref="CrudService{TEntity}"/>
        public MalfunctionService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<Malfunction>> logger,
            IMalfunctionRepository repository) : base(unitOfWork, logger, repository) { }
        
        public override Task<IEnumerable<Malfunction>> SearchAsync(string search)
        {
            search = search.ToUpperInvariant();
            return _unitOfWork.MalfunctionRepository.GetAllAsync(a =>
                a.Name.ToUpperInvariant().Contains(search)
                || search.Contains(a.Name.ToUpperInvariant()));
        }
    }
}
