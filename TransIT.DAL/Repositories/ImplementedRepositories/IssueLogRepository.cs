using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class IssueLogRepository : BaseRepository<IssueLog>, IIssueLogRepository
    {
        public IssueLogRepository(DbContext context)
            : base(context)
        {
        }

        public override Task<IQueryable<IssueLog>> SearchExpressionAsync(IEnumerable<string> strs) =>
            Task.FromResult(
                GetQueryable().Where(entity =>
                    strs.Any(str => entity.Description.ToUpperInvariant().Contains(str)
                    || entity.NewState.Name.ToUpperInvariant().Contains(str)
                    || entity.OldState.Name.ToUpperInvariant().Contains(str)))
                );
        
        protected override IQueryable<IssueLog> ComplexEntities => Entities
            .Include(t => t.ActionType)
            .Include(z => z.Create)
            .Include(a => a.Issue)
            .ThenInclude(x => x.Vehicle)
            .Include(x => x.Issue)
            .ThenInclude(x => x.Malfunction)
            .Include(x => x.Issue)
            .ThenInclude(x => x.AssignedTo)
            .Include(x => x.Issue)
            .ThenInclude(x => x.State)
            .Include(b => b.Mod)
            .Include(c => c.NewState)
            .Include(d => d.OldState)
            .Include(e => e.Supplier)
            .OrderByDescending(u => u.ModDate)
            .ThenByDescending(x => x.CreateDate);
    }
}
