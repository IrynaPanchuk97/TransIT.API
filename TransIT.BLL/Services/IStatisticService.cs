using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.ViewModels;

namespace TransIT.BLL.Services
{
    public interface IStatisticService
    {
        IEnumerable<VehicleTypeMalfunctionGroup> GetStatisticGroup();
        Task<IEnumerable<VehicleTypeMalfunctionSubgroup>> GetStatisticSubGroup(int groupId);
    }
}
