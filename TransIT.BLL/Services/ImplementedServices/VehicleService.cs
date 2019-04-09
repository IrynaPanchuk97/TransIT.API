﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TransIT.BLL.Services.InterfacesRepositories;
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
        
        public override Task<IEnumerable<Vehicle>> SearchAsync(string search)
        {
            search = search.ToUpperInvariant();
            try
            {
                return _unitOfWork.VehicleRepository.GetAllAsync(a =>
                    a.Brand.ToUpperInvariant().Contains(search)
                    || a.RegNum.ToUpperInvariant().Contains(search)
                    || a.InventoryId.ToUpperInvariant().Contains(search)
                    || a.Model.ToUpperInvariant().Contains(search)
                    || a.Vincode.ToUpperInvariant().Contains(search)
                    || search.Contains(a.Brand.ToUpperInvariant())
                    || search.Contains(a.RegNum.ToUpperInvariant())
                    || search.Contains(a.InventoryId.ToUpperInvariant())
                    || search.Contains(a.Model.ToUpperInvariant())
                    || search.Contains(a.Vincode.ToUpperInvariant()));
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(SearchAsync));
                return null;
            }
        }
    }
}
