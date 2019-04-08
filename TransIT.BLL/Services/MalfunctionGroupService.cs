using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services
{
    /// <summary>
    /// Malfunction Group Crud service
    /// </summary>
    public class MalfunctionGroupService : ICrudService<MalfunctionGroup>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MalfunctionGroupService> _logger;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        public MalfunctionGroupService(IUnitOfWork unitOfWork, ILogger<MalfunctionGroupService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Get model by id
        /// </summary>
        /// <param name="id">Id of the model to take</param>
        /// <returns>Founded model or null on failure</returns>
        public Task<MalfunctionGroup> GetAsync(int id) =>
            _unitOfWork.MalfunctionGroupRepository.GetByIdAsync(id);

        /// <summary>
        /// Get enumerable of models
        /// </summary>
        /// <param name="offset">How many models to skip</param>
        /// <param name="size">How many models to take</param>
        /// <returns>Enumerable of models</returns>
        public Task<IEnumerable<MalfunctionGroup>> GetRangeAsync(uint offset, uint size) =>
            _unitOfWork.MalfunctionGroupRepository.GetRangeAsync(offset, size);

        /// <summary>
        /// Create model
        /// </summary>
        /// <param name="malfunctionGroup">Model with properties to create</param>
        /// <returns>Created model or null on failure</returns>
        public async Task<MalfunctionGroup> CreateAsync(MalfunctionGroup malfunctionGroup)
        {
            try
            {
                var tmp = _unitOfWork.MalfunctionGroupRepository;
                await _unitOfWork.MalfunctionGroupRepository.AddAsync(malfunctionGroup);
                await _unitOfWork.SaveAsync();
                return malfunctionGroup;
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, nameof(CreateAsync), e.Entries);
                return null;
            }
            catch(Exception e)
            {
                _logger.LogError(e, nameof(CreateAsync));
                return null;
            }
        }

        /// <summary>
        /// Update model
        /// </summary>
        /// <param name="malfunctionGroup">Model with properties to be updated</param>
        /// <returns>Updated model or null on failure</returns>
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
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UpdateAsync));
                return null;
            }
        }

        /// <summary>
        /// Deletes model by id
        /// </summary>
        /// <param name="id">Id of model to be deleted</param>
        /// <returns>Nothing</returns>
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
            catch (Exception e)
            {
                _logger.LogError(e, nameof(CreateAsync));
            }
        }
    }
}
