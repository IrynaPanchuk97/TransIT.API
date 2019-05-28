using System.Collections.Generic;
using System.Linq;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.BLL.Services
{
    public class StatisticService
    {
        protected readonly IIssueRepository _issueRepository;
        protected readonly IVehicleTypeRepository _vehicleTypeRepository;
        protected readonly IVehicleRepository _vehicleRepository;
        protected readonly IMalfunctionRepository _malfunctionRepository;
        protected readonly IMalfunctionGroupRepository _malfunctionGroupRepository;
        protected readonly IMalfunctionSubgroupRepository _malfunctionSubGroupRepository;

        public StatisticService(
            IIssueRepository issueRepository,
            IVehicleTypeRepository vehicleTypeRepository,
            IVehicleRepository vehicleRepository,
            IMalfunctionRepository malfunctionRepository,
            IMalfunctionGroupRepository malfunctionGroupRepository,
            IMalfunctionSubgroupRepository malfunctionSubGroupRepository)
        {
            _issueRepository = issueRepository;
            _vehicleTypeRepository = vehicleTypeRepository;
            _malfunctionGroupRepository = malfunctionGroupRepository;
            _malfunctionSubGroupRepository = malfunctionSubGroupRepository;
        }

        public void GetStatisticGroup()
        {
            var issues = _issueRepository.GetQueryable();
            var vehicles = _vehicleRepository.GetQueryable();
            var vehicleTypes = _vehicleTypeRepository.GetQueryable();
            var malfunctions = _malfunctionRepository.GetQueryable();
            var malfunctionSubgroups = _malfunctionSubGroupRepository.GetQueryable();
            var malfunctionGroups = _malfunctionGroupRepository.GetQueryable();

            var res =
                from i in issues
                join v in vehicles on i.VehicleId equals v.Id
                join vt in vehicleTypes on v.VehicleTypeId equals vt.Id
                join m in malfunctions on i.MalfunctionId equals m.Id
                join ms in malfunctionSubgroups on m.MalfunctionSubgroupId equals ms.Id
                join mg in malfunctionGroups on ms.MalfunctionGroupId equals mg.Id
                select new
                {
                    vt, mg, i
                } into joined
                group joined by new { malfunctionGroup = joined.mg.Id, vehicleType = joined.vt.Id } into grouped
                select grouped;

            
           // _issueRepository.GetQueryable().GroupBy().Where(issue =>
           //     _vehicleTypeRepository.GetQueryable().Where(type =>
           //         _malfunctionGroupRepository.GetQueryable().Where(group =>
           //            issue.Vehicle.VehicleTypeId == type.Id
           //            && issue.Malfunction.MalfunctionSubgroup.MalfunctionGroupId == group.Id)
           //    )
           //)
        }

        public void GetStatisticSubGroup(int groupId)
        {
            var issues = _issueRepository.GetQueryable();
            var vehicles = _vehicleRepository.GetQueryable();
            var vehicleTypes = _vehicleTypeRepository.GetQueryable();
            var malfunctions = _malfunctionRepository.GetQueryable();
            var malfunctionSubgroups = _malfunctionSubGroupRepository.GetQueryable();
            var malfunctionGroups = _malfunctionGroupRepository.GetQueryable();

            var res =
                 from i in issues
                 join v in vehicles on i.VehicleId equals v.Id
                 join vt in vehicleTypes on v.VehicleTypeId equals vt.Id
                 join m in malfunctions on i.MalfunctionId equals m.Id
                 join ms in malfunctionSubgroups on m.MalfunctionSubgroupId equals ms.Id
                 join mg in malfunctionGroups on ms.MalfunctionGroupId equals mg.Id
                 select new
                 {
                     vt,
                     mg,
                     i
                 } into joined
                 group joined by new { malfunctionGroup = joined.mg.Id, vehicleType = joined.vt.Id } into grouped
                 select new { };

        }

    }
}
