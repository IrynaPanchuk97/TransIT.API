using System;
using System.Collections.Generic;
using System.Text;
using TransIT.DAL.Models;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class DocumentRepository : BaseRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(DBContext context)
               : base(context)
        {
            _ = context.Set<Document>();
        }
    }
}
