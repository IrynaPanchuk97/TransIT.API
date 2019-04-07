using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services
{
    public class MalfunctionGroupService : IMalfunctionGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MalfunctionGroupService> _logger;
        public MalfunctionGroupService(IUnitOfWork unitOfWork, ILogger<MalfunctionGroupService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public Task<MalfunctionGroup> GetAsync(int id) =>
            _unitOfWork.MalfunctionGroupRepository.GetByIdAsync(id);

        public Task<IEnumerable<MalfunctionGroup>> GetAllAsync(uint offset, uint size) =>
            _unitOfWork.MalfunctionGroupRepository.GetRangeAsync(offset, size);

        public async Task<MalfunctionGroup> CreateAsync(MalfunctionGroup malfunctionGroup)
        {
            try
            {
                await _unitOfWork.MalfunctionGroupRepository.AddAsync(malfunctionGroup);
                await _unitOfWork.SaveAsync();
                return malfunctionGroup;
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, nameof(CreateAsync), e.Entries);
                return null;
            }
        }

        public async Task<MalfunctionGroup> UpdateAsync(MalfunctionGroup malfunctionGroup)
        {
            try
            {
                _unitOfWork.MalfunctionGroupRepository.Update(malfunctionGroup);
                await _unitOfWork.SaveAsync();
                return malfunctionGroup;
            }
            catch(DbUpdateException e)
            {
                _logger.LogError(e, nameof(DeleteAsync), e.Entries);
                return null;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var group = new MalfunctionGroup { Id = id };
                _unitOfWork.MalfunctionGroupRepository.Remove(group);
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, nameof(DeleteAsync), e.Entries);
            }
        }
    }
}
