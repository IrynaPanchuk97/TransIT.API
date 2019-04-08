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
    /// User model CRUD
    /// </summary>
    /// <see cref="IActionTypeService"/>

    public class ActionTypeService : IActionTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ActionTypeService> _logger;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        public ActionTypeService(IUnitOfWork unitOfWork, ILogger<ActionTypeService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        /// <summary>
        /// Get model by id
        /// </summary>
        /// <param name="id">Id of the model to take</param>
        /// <returns>Founded model or null on failure</returns>
        public Task<ActionType> GetAsync(int id)
        {
            return _unitOfWork.ActionTypeRepository.GetByIdAsync(id);
        }
        /// <summary>
        /// Get enumerable of models
        /// </summary>
        /// <param name="offset">How many models to skip</param>
        /// <param name="size">How many models to take</param>
        /// <returns>Enumerable of models</returns>
        public Task<IEnumerable<ActionType>> GetRangeAsync(uint offset, uint size)
        {
            return _unitOfWork.ActionTypeRepository.GetRangeAsync(offset, size);
        }
        /// <summary>
        /// Create model
        /// </summary>
        /// <param name="actionType">Model with properties to create</param>
        /// <returns>Created model or null on failure</returns>
        public async Task<ActionType> CreateAsync(ActionType actionType)
        {
            try
            {
                var tmp = _unitOfWork.ActionTypeRepository;
                await _unitOfWork.ActionTypeRepository.AddAsync(actionType);
                await _unitOfWork.SaveAsync();
                return actionType;
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
        /// <param name="actionType">Model with properties to be updated</param>
        /// <returns>Updated model or null on failure</returns>
        public async Task<ActionType> UpdateAsync(ActionType actionType)
        {
            try
            {
                _unitOfWork.ActionTypeRepository.Update(actionType);
                await _unitOfWork.SaveAsync();
                return actionType;
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
                var action = new ActionType { Id = id };
                _unitOfWork.ActionTypeRepository.Remove(action);
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
    }
}
