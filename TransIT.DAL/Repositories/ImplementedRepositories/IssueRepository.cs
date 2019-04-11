using Microsoft.EntityFrameworkCore;
using System.Linq;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class IssueRepository : BaseRepository<Issue>, IIssueRepository
    {
        public IssueRepository(DbContext context)
               : base(context)
        {
        }

        protected override IQueryable<Issue> ComplexEntities => Entities.
            Include(t => t.AssignedToNavigation).
            Include(e => e.Create).
            Include(x => x.Malfunction).
            Include(x => x.Mod).
            Include(x => x.State).
            Include(x => x.Vehicle);        

    }
}
