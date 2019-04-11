using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities.Abstractions;
using TransIT.DAL.Repositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services
{
    /// <summary>
    /// Entity CRUD service
    /// </summary>
    /// <see cref="ICrudService{T}"/>
    public abstract class CrudService<TEntity> : ICrudService<TEntity> where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Saves changes
        /// </summary>
        protected readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Logs on error
        /// </summary>
        protected readonly ILogger<CrudService<TEntity>> _logger;

        /// <summary>
        /// CRUD operations on entity
        /// </summary>
        protected readonly IBaseRepository<TEntity> _repository;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        /// <param name="repository">CRUD operations on model</param>
        public CrudService(IUnitOfWork unitOfWork, ILogger<CrudService<TEntity>> logger, IBaseRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Get model by id
        /// </summary>
        /// <param name="id">Id of the model to take</param>
        /// <returns>Founded model or null on failure</returns>
        public virtual Task<TEntity> GetAsync(int id) => _repository.GetByIdAsync(id);

        /// <summary>
        /// Get enumerable of models
        /// </summary>
        /// <param name="offset">How many models to skip</param>
        /// <param name="size">How many models to take</param>
        /// <returns>Enumerable of models</returns>
        public virtual Task<IEnumerable<TEntity>> GetRangeAsync(uint offset, uint size) => _repository.GetRangeAsync(offset, size);

        /// <summary>
        /// Create model
        /// </summary>
        /// <param name="model">Model with properties to create</param>
        /// <returns>Created model or null on failure</returns>
        public virtual async Task<TEntity> CreateAsync(TEntity model)
        {
            try
            {
                await _repository.AddAsync(model);
                await _unitOfWork.SaveAsync();
                return model;
            }
            catch (DbUpdateException e)
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

        /// <summary>
        /// Update model
        /// </summary>
        /// <param name="model">Model with properties to be updated</param>
        /// <returns>Updated model or null on failure</returns>
        public virtual async Task<TEntity> UpdateAsync(TEntity model)
        {
            try
            {
                _repository.Update(model);
                await _unitOfWork.SaveAsync();
                return model;
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

        /// <summary>
        /// Deletes model by id
        /// </summary>
        /// <param name="id">Id of model to be deleted</param>
        /// <returns>Nothing</returns>
        public virtual async Task DeleteAsync(int id)
        {
            try
            {
                var model = new TEntity { Id = id };
                _repository.Remove(model);
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

        /// <summary>
        /// Searches for matches
        /// </summary>
        /// <param name="search">String to search</param>
        /// <returns>All matches</returns>
        /// <summary>
        /// Searches for matches
        /// </summary>
        /// <param name="search">String to search</param>
        /// <returns>All matches</returns>
        public async virtual Task<IEnumerable<TEntity>> SearchAsync(string search)
        {
            var words = search
                .Split(' ', ',', '.')
                .Select(x => x.Trim().ToUpperInvariant());
            try
            {
                return await SearchExpressionAsync(words);
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(SearchAsync));
                return null;
            }
        }
        
        /// <summary>
        /// Expression to find matches
        /// </summary>
        /// <param name="strs">Trimmed strings</param>
        /// <returns>Matched entities</returns>
        protected abstract Task<IEnumerable<TEntity>> SearchExpressionAsync(IEnumerable<string> strs);
    }
}
