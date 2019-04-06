using Microsoft.EntityFrameworkCore;
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
    }
}
