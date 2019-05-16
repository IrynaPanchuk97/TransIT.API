using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
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

        protected override Task<IEnumerable<VehicleType>> SearchExpressionAsync(IEnumerable<string> strs) =>
            _unitOfWork.VehicleTypeRepository.GetAllAsync(entity =>
                strs.Any(str => entity.Name.ToUpperInvariant().Contains(str)));
    }
}

