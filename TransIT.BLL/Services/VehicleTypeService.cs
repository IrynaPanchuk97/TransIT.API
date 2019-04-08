using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services
{
    public class VehicleTypeService : IVehicleTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<VehicleTypeService> _logger;

        public VehicleTypeService(ILogger<VehicleTypeService> logger, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;           
            _logger = logger;
        }

        public Task<VehicleType> GetAsync(int vehicleTypeId) =>
              _unitOfWork.VehicleTypeRepository.GetByIdAsync(vehicleTypeId);

        public Task<IEnumerable<VehicleType>> GetAllAsync(uint offset, uint amount) =>
            _unitOfWork.VehicleTypeRepository.GetRangeAsync(offset, amount);
        
        public async Task<VehicleType> CreateAsync(VehicleType vehicleType)
        {
            if ((await _unitOfWork.VehicleTypeRepository.GetAllAsync(p =>
                    p.Name == vehicleType.Name)).Any())
                return null;

            try
            {
                await _unitOfWork.VehicleTypeRepository.AddAsync(vehicleType);
                await _unitOfWork.SaveAsync();
                return vehicleType;
            }
            catch  (DbUpdateException e)
            {
                _logger.LogError(e, nameof(CreateAsync), e.Entries);
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(CreateAsync));
                throw e;
            }
            
        }

        public async Task<VehicleType> UpdateAsync(VehicleType vehicleType)
        {
            if (await _unitOfWork.VehicleTypeRepository.GetByIdAsync(vehicleType.Id) == null)
                return null;
            try
            {
                _unitOfWork.VehicleTypeRepository.Update(vehicleType);
                await _unitOfWork.SaveAsync();
                return vehicleType;

            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, nameof(UpdateAsync), e.Entries);
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UpdateAsync));
                throw e;
            }
        }
        
        public async Task DeleteAsync(int vehicleTypeId)
        {
            try
            {
                _unitOfWork.VehicleTypeRepository.Remove(new VehicleType { Id = vehicleTypeId });
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, nameof(DeleteAsync), e.Entries);
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(DeleteAsync));
                throw e;
            }
        }
    }
}
