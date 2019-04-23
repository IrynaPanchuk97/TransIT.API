using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Services.InterfacesRepositories
{
    /// <summary>
    /// Document type model CRUD
    /// </summary>
    public interface IDocumentService : ICrudService<Document>
    {
        Task<IEnumerable<Document>> GetRangeByIssueLogIdAsync(int issueLogId);
    }
}
