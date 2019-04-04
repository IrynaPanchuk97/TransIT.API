using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class MalfunctionRepository : BaseRepository<Malfunction>, IMalfunctionRepository
    {
        public MalfunctionRepository(DbContext context)
               : base(context)
        {
        }
    }
}
