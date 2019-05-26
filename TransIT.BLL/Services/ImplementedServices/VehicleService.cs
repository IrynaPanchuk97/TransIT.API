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
    /// Service for Vehicle
    /// </summary>
    public class VehicleService : CrudService<Vehicle>, IVehicleService
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        public VehicleService(IUnitOfWork unitOfWork,
            ILogger<CrudService<Vehicle>> logger,
            IVehicleRepository repository) : base(unitOfWork, logger, repository) { }
        
        protected override Task<IEnumerable<Vehicle>> SearchExpressionAsync(IEnumerable<string> strs) =>
            _unitOfWork.VehicleRepository.GetAllAsync(entity =>
                strs.Any(str => entity.Brand.ToUpperInvariant().Contains(str)
                || entity.RegNum.ToUpperInvariant().Contains(str)
                || entity.InventoryId.ToUpperInvariant().Contains(str)
                || entity.Model.ToUpperInvariant().Contains(str)
                || entity.Vincode.ToUpperInvariant().Contains(str)
                || entity.VehicleType.Name.ToUpperInvariant().Contains(str)));
    }
}
