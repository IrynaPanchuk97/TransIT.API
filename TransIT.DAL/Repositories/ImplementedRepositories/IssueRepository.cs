using System.Collections.Generic;
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
            int previousMax = await Entities.DefaultIfEmpty().MaxAsync(i => i.Number) ?? 0;
            issue.Number = previousMax + 1;

            return await base.AddAsync(issue);
        }

        public override Task<IQueryable<Issue>> SearchExpressionAsync(IEnumerable<string> strs) =>
            Task.FromResult(
                GetQueryable().Where(entity =>
                    strs.Any(str => !string.IsNullOrEmpty(entity.Summary) && entity.Summary.ToUpperInvariant().Contains(str)
                    || !string.IsNullOrEmpty(entity.Malfunction.Name) && entity.Malfunction.Name.ToUpperInvariant().Contains(str)
                    || !string.IsNullOrEmpty(entity.Malfunction.MalfunctionSubgroup.Name) && entity.Malfunction.MalfunctionSubgroup.Name.ToUpperInvariant().Contains(str)
                    || !string.IsNullOrEmpty(entity.Malfunction.MalfunctionSubgroup.MalfunctionGroup.Name) && entity.Malfunction.MalfunctionSubgroup.MalfunctionGroup.Name.ToUpperInvariant().Contains(str)
                    || !string.IsNullOrEmpty(entity.Number.ToString()) && entity.Number.ToString().ToUpperInvariant().Contains(str)
                    || !string.IsNullOrEmpty(entity.State.TransName) && entity.State.TransName.ToUpperInvariant().Contains(str)
                    || !string.IsNullOrEmpty(entity.Vehicle.Model) && entity.Vehicle.Model.ToUpperInvariant().Contains(str)))
                );
        
        protected override IQueryable<Issue> ComplexEntities => Entities
            .Include(i => i.AssignedTo)
            .Include(i => i.Create)
            .Include(i => i.Malfunction)
                .ThenInclude(m => m.MalfunctionSubgroup)
                    .ThenInclude(s => s.MalfunctionGroup)
            .Include(i => i.Mod)
            .Include(i => i.State)
            .Include(i => i.Vehicle)
                .ThenInclude(n => n.Location).OrderByDescending(u => u.ModDate).ThenByDescending(x => x.CreateDate);        

    }
}
