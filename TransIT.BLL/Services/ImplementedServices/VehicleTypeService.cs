using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services
{
    /// <summary>
    /// Service for Vehicle Type
    /// </summary>
    public class VehicleTypeService : CrudService<VehicleType>, IVehicleTypeService
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        public VehicleTypeService(
            IUnitOfWork unitOfWork,
            ILogger<CrudService<VehicleType>> logger,
            IVehicleTypeRepository repository) : base(unitOfWork, logger, repository) { }
        
        public override Task<IEnumerable<VehicleType>> SearchAsync(string search)
        {
            search = search.ToUpperInvariant();
            return _unitOfWork.VehicleTypeRepository.GetAllAsync(a =>
                a.Name.ToUpperInvariant().Contains(search)
                || search.Contains(a.Name.ToUpperInvariant()));
        }
    }
}

