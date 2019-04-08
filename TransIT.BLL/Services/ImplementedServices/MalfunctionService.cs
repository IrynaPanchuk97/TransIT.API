using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    public class MalfunctionService : IMalfunctionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MalfunctionService> _logger;

        public MalfunctionService(IUnitOfWork unitOfWork, ILogger<MalfunctionService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Malfunction> CreateAsync(Malfunction malfunction)
        {
            try
            {
                var tmp = _unitOfWork.MalfunctionRepository;
                await _unitOfWork.MalfunctionRepository.AddAsync(malfunction);
                await _unitOfWork.SaveAsync();
                return malfunction;
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, nameof(CreateAsync), e.Entries);
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(CreateAsync));
                return null;
            }
        }

        public async Task<Malfunction> UpdateAsync(Malfunction malfunction)
        {
            try
            {
                _unitOfWork.MalfunctionRepository.Update(malfunction);
                await _unitOfWork.SaveAsync();
                return malfunction;
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, nameof(UpdateAsync), e.Entries);
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UpdateAsync));
                return null;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var group = new Malfunction { Id = id };
                _unitOfWork.MalfunctionRepository.Remove(group);
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, nameof(DeleteAsync), e.Entries);
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(DeleteAsync));
            }
        }

        public Task<Malfunction> GetAsync(int id) =>
            _unitOfWork.MalfunctionRepository.GetByIdAsync(id);

        public Task<IEnumerable<Malfunction>> GetRangeAsync(uint offset, uint amount) =>
        _unitOfWork.MalfunctionRepository.GetRangeAsync(offset, amount);
    }
}
