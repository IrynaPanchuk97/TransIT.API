﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Models.ViewModels;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.BLL.Services
{
    public class StatisticService : IStatisticService
    {
        protected readonly IQueryable<Issue> _issues;
        protected readonly IQueryable<VehicleType> _vehicleTypes;
        protected readonly IQueryable<Vehicle> _vehicles;
        protected readonly IQueryable<Malfunction> _malfunctions;
        protected readonly IQueryable<MalfunctionGroup> _malfunctionGroups;
        protected readonly IQueryable<MalfunctionSubgroup> _malfunctionSubgroups;

        public StatisticService(
            IIssueRepository issueRepository,
            IVehicleTypeRepository vehicleTypeRepository,
            IVehicleRepository vehicleRepository,
            IMalfunctionRepository malfunctionRepository,
            IMalfunctionGroupRepository malfunctionGroupRepository,
            IMalfunctionSubgroupRepository malfunctionSubGroupRepository)
        {
            _issues = issueRepository.GetQueryable().AsNoTracking();
            _vehicles = vehicleRepository.GetQueryable().AsNoTracking();
            _vehicleTypes = vehicleTypeRepository.GetQueryable().AsNoTracking();
            _malfunctions = malfunctionRepository.GetQueryable().AsNoTracking();
            _malfunctionSubgroups = malfunctionSubGroupRepository.GetQueryable().AsNoTracking();
            _malfunctionGroups = malfunctionGroupRepository.GetQueryable().AsNoTracking();
        }


        public Task<IEnumerable<VehicleTypeMalfunctionGroup>> GetStatisticGroup() =>
            Task.FromResult<IEnumerable<VehicleTypeMalfunctionGroup>>(from j in from i in _issues
                                      join vt in _vehicleTypes on i.Vehicle.VehicleTypeId equals vt.Id
                                      join mg in _malfunctionGroups on i.Malfunction.MalfunctionSubgroup.MalfunctionGroupId equals mg.Id
                                      select new { vt, mg, i }
                            group j by new { malfunctionGroup = j.mg.Id, vehicleType = j.vt.Id } into g
                            select new VehicleTypeMalfunctionGroup
                            {
                                VehicleType = _vehicleTypes.FirstOrDefault(x => x.Id == g.Key.vehicleType),
                                Group = _malfunctionGroups.FirstOrDefault(x => x.Id == g.Key.malfunctionGroup),
                                Count = (ulong)(from i in _issues
                                                where i.Malfunction.MalfunctionSubgroup.MalfunctionGroupId == g.Key.malfunctionGroup
                                                select i).LongCount()
                            });


        public Task<IEnumerable<VehicleTypeMalfunctionSubgroup>> GetStatisticSubGroup(int groupId) =>
             Task.FromResult<IEnumerable<VehicleTypeMalfunctionSubgroup>>(from j in from i in _issues
                                       join vt in _vehicleTypes on i.Vehicle.VehicleTypeId equals vt.Id
                                       join msg in _malfunctionSubgroups on i.Malfunction.MalfunctionSubgroupId equals msg.Id
                                       where msg.MalfunctionGroupId == groupId
                                       select new { vt, msg, i }
                             group j by new { malfunctionSubgroup = j.msg.Id, vehicleType = j.vt.Id } into g
                             select new VehicleTypeMalfunctionSubgroup
                             {
                                 VehicleType = _vehicleTypes.FirstOrDefault(x => x.Id == g.Key.vehicleType),
                                 Subgroup = _malfunctionSubgroups.FirstOrDefault(x => x.Id == g.Key.malfunctionSubgroup),
                                 Count = (ulong)(from i in _issues
                                                 where i.Malfunction.MalfunctionSubgroup.MalfunctionGroupId == g.Key.malfunctionSubgroup
                                                 select i).LongCount()
                             });

    }
}
