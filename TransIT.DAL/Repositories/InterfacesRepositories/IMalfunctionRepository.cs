using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TransIT.DAL.Models;

namespace TransIT.DAL.Repositories.InterfacesRepositories
{
    interface IMalfunctionRepository : IBaseRepository<Malfunction>
    {
    }
}
