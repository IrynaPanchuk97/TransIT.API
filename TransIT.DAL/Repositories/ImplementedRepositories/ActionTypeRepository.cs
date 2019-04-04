using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TransIT.DAL.Models;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class ActionTypeRepository : BaseRepository<ActionType>, IActionTypeRepository
    {
        public ActionTypeRepository(DBContext context)
               : base(context)
        {
            _ = context.Set<ActionType>();
        }
    }
}
