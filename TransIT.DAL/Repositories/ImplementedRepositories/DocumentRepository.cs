using Microsoft.EntityFrameworkCore;
using System.Linq;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class DocumentRepository : BaseRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(DbContext context)
            : base(context)
        {
        }

        protected override IQueryable<Document> ComplexEntities => Entities
            .Include(t => t.Create)
            .Include(z => z.IssueLog)
            .ThenInclude(x => x.Issue.State)
            .Include(x => x.IssueLog)
            .ThenInclude(x => x.Issue.Vehicle)
            .ThenInclude(x => x.VehicleType)
            .Include(z => z.IssueLog)
            .ThenInclude(x => x.Supplier)
            .Include(z => z.IssueLog)
            .ThenInclude(x => x.ActionType)
            .Include(z => z.IssueLog)
            .ThenInclude(x => x.Issue.Malfunction)
            .Include(z => z.IssueLog)
            .ThenInclude(x => x.NewState)
            .Include(z => z.IssueLog)
            .ThenInclude(x => x.OldState)
            .Include(z => z.IssueLog)
            .ThenInclude(x => x.Issue.AssignedToNavigation)
            .Include(a => a.Mod)
            .OrderByDescending(u => u.ModDate)
            .ThenByDescending(x => x.CreateDate);
    }
}
