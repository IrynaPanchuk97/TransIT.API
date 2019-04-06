using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class ActionTypeRepository : BaseRepository<ActionType>, IActionTypeRepository
    {
        public ActionTypeRepository(DbContext context)
               : base(context)
        {            
        }
    }
}
