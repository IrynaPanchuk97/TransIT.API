using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
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

        public override async Task<Issue> AddAsync(Issue issue)
        {
            int previousMax = Entities.DefaultIfEmpty().Max(i => i.Number) ?? 0;
            issue.Number = previousMax + 1;

            return await base.AddAsync(issue);
        }

        protected override IQueryable<Issue> ComplexEntities => Entities
            .Include(i => i.AssignedToNavigation)
            .Include(i => i.Create)
            .Include(i => i.Malfunction)
                .ThenInclude(m => m.MalfunctionSubgroup)
                    .ThenInclude(s => s.MalfunctionGroup)
            .Include(i => i.Mod)
            .Include(i => i.State)
            .Include(i => i.Vehicle).OrderByDescending(u => u.ModDate).ThenByDescending(x => x.CreateDate);        

    }
}
