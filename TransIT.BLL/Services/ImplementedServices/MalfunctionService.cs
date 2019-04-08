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
    /// <summary>
    /// Malfunction CRUD service
    /// </summary>
    /// <see cref="IMalfunctionService"/>
    public class MalfunctionService : IMalfunctionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MalfunctionService> _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        public MalfunctionService(IUnitOfWork unitOfWork, ILogger<MalfunctionService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Create model
        /// </summary>
        /// <param name="malfunction">Model with properties to create</param>
        /// <returns>Created model or null on failure</returns>
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

        /// <summary>
        /// Update model
        /// </summary>
        /// <param name="malfunction">Model with properties to be updated</param>
        /// <returns>Updated model or null on failure</returns>
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

        /// <summary>
        /// Deletes model by id
        /// </summary>
        /// <param name="id">Id of model to be deleted</param>
        /// <returns>Nothing</returns>
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

        /// <summary>
        /// Get model by id
        /// </summary>
        /// <param name="id">Id of the model to take</param>
        /// <returns>Founded model or null on failure</returns>
        public Task<Malfunction> GetAsync(int id) =>
            _unitOfWork.MalfunctionRepository.GetByIdAsync(id);

        /// <summary>
        /// Get enumerable of models
        /// </summary>
        /// <param name="offset">How many models to skip</param>
        /// <param name="amount">How many models to take</param>
        /// <returns>Enumerable of models</returns>
        public Task<IEnumerable<Malfunction>> GetRangeAsync(uint offset, uint amount) =>
        _unitOfWork.MalfunctionRepository.GetRangeAsync(offset, amount);
    }
}
