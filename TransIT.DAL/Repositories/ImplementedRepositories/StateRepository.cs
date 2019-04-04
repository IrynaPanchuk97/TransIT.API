using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class StateRepository : BaseRepository<State>, IStateRepository
    {
        public StateRepository(DbContext context)
               : base(context)
        {
        }
    }
}
