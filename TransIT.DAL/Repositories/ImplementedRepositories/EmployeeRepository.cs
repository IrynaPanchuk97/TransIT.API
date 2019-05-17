using Microsoft.EntityFrameworkCore;
using System.Linq;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext context)
               : base(context)
        {
        }

        protected override IQueryable<Employee> ComplexEntities => Entities
            .Include(e => e.Create)
            .Include(e => e.Mod).OrderByDescending(u => u.ModDate).ThenByDescending(x => x.CreateDate)
            .Include(e => e.Post);
    }
}
