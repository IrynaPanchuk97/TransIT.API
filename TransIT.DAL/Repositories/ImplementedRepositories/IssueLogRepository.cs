using Microsoft.EntityFrameworkCore;
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
    }
}
