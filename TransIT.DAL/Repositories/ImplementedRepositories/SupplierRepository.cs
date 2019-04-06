using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class SupplierRepository: BaseRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(DbContext context)
               : base(context)
        {
        }
    }
}
