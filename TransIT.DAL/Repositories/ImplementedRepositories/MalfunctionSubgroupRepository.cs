using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class MalfunctionSubgroupRepository : BaseRepository<MalfunctionSubgroup>, IMalfunctionSubgroupRepository
    {
        public MalfunctionSubgroupRepository(DbContext context)
            : base(context)
        {
        }
    }
}
