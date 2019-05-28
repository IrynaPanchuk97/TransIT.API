using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Models.ViewModels;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.BLL.Services
{
    public class StatisticService
    {
        protected readonly IQueryable<Issue> _issues;
        protected readonly IQueryable<VehicleType> _vehicleTypes;
        protected readonly IQueryable<Vehicle> _vehicles;
        protected readonly IQueryable<Malfunction> _malfunctions;
        protected readonly IQueryable<MalfunctionGroup> _malfunctionGroups;
        protected readonly IQueryable<MalfunctionSubgroup> _malfunctionSubgroups;
        
//        protected readonly IIssueRepository _issueRepository;
//        protected readonly IVehicleTypeRepository _vehicleTypeRepository;
//        protected readonly IVehicleRepository _vehicleRepository;
//        protected readonly IMalfunctionRepository _malfunctionRepository;
//        protected readonly IMalfunctionGroupRepository _malfunctionGroupRepository;
//        protected readonly IMalfunctionSubgroupRepository _malfunctionSubGroupRepository;

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

//            _issueRepository = issueRepository;
//            _vehicleTypeRepository = vehicleTypeRepository;
//            _malfunctionGroupRepository = malfunctionGroupRepository;
//            _malfunctionSubGroupRepository = malfunctionSubGroupRepository;
        }

//        public void GetStatisticGroup()
//        {
//            var issues = _issueRepository.GetQueryable();
//            var vehicles = _vehicleRepository.GetQueryable();
//            var vehicleTypes = _vehicleTypeRepository.GetQueryable();
//            var malfunctions = _malfunctionRepository.GetQueryable();
//            var malfunctionSubgroups = _malfunctionSubGroupRepository.GetQueryable();
//            var malfunctionGroups = _malfunctionGroupRepository.GetQueryable();
//
//            var res =
//                from i in issues
//                join v in vehicles on i.VehicleId equals v.Id
//                join vt in vehicleTypes on v.VehicleTypeId equals vt.Id
//                join m in malfunctions on i.MalfunctionId equals m.Id
//                join ms in malfunctionSubgroups on m.MalfunctionSubgroupId equals ms.Id
//                join mg in malfunctionGroups on ms.MalfunctionGroupId equals mg.Id
//                select new
//                {
//                    vt, mg, i
//                } into joined
//                group joined by new { malfunctionGroup = joined.mg.Id, vehicleType = joined.vt.Id } into grouped
//                select grouped;
//
            
           // _issueRepository.GetQueryable().GroupBy().Where(issue =>
           //     _vehicleTypeRepository.GetQueryable().Where(type =>
           //         _malfunctionGroupRepository.GetQueryable().Where(group =>
           //            issue.Vehicle.VehicleTypeId == type.Id
           //            && issue.Malfunction.MalfunctionSubgroup.MalfunctionGroupId == group.Id)
           //    )
           //)
//        }

//        public void GetStatisticSubGroup(int groupId)
//        {
//            var issues = _issueRepository.GetQueryable().AsNoTracking();
//            var vehicles = _vehicleRepository.GetQueryable().AsNoTracking();
//            var vehicleTypes = _vehicleTypeRepository.GetQueryable().AsNoTracking();
//            var malfunctions = _malfunctionRepository.GetQueryable().AsNoTracking();
//            var malfunctionSubgroups = _malfunctionSubGroupRepository.GetQueryable().AsNoTracking();
//            var malfunctionGroups = _malfunctionGroupRepository.GetQueryable().AsNoTracking();
//
//            var joined =
//                from i in issues
//                join v in vehicles on i.VehicleId equals v.Id
//                join vt in vehicleTypes on v.VehicleTypeId equals vt.Id
//                join m in malfunctions on i.MalfunctionId equals m.Id
//                join ms in malfunctionSubgroups on m.MalfunctionSubgroupId equals ms.Id
//                join mg in malfunctionGroups on ms.MalfunctionGroupId equals mg.Id
//                select new { vt, mg, i };
//            
//            var counted =
//                from j in joined
//                group j by new { malfunctionGroup = j.mg.Id, vehicleType = j.vt.Id } into g
//                select new
//                {
//                    Count =
//                        (from i in issues
//                        where i.Malfunction.MalfunctionSubgroup.MalfunctionGroupId == g.Key.malfunctionGroup
//                        select i).LongCount(),
//                    VehicleType = g.Key.vehicleType,
//                    MalfunctionGroup = g.Key.malfunctionGroup
//                };
//        }

//        public dynamic GetStatisticSubGroup(int groupId) =>
//            from j in from i in _issues
//                join v in _vehicles on i.VehicleId equals v.Id
//                join vt in _vehicleTypes on v.VehicleTypeId equals vt.Id
//                join m in _malfunctions on i.MalfunctionId equals m.Id
//                join ms in _malfunctionSubgroups on m.MalfunctionSubgroupId equals ms.Id
//                join mg in _malfunctionGroups on ms.MalfunctionGroupId equals mg.Id
//                select new { vt, mg, i }
//            group j by new { malfunctionGroup = j.mg.Id, vehicleType = j.vt.Id } into g
//            select new
//            {
//                Count =
//                    (from i in _issues
//                        where i.Malfunction.MalfunctionSubgroup.MalfunctionGroupId == g.Key.malfunctionGroup
//                        select i).LongCount(),
//                VehicleType = g.Key.vehicleType,
//                MalfunctionGroup = g.Key.malfunctionGroup
//            };

        public dynamic GetStatisticGroup() =>
            from j in from i in _issues
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
            };
    }
}
