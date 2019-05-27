using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(DbContext context)
               : base(context)
        {
        }
        
        public override Task<IQueryable<Supplier>> SearchExpressionAsync(IEnumerable<string> strs) =>
            Task.FromResult(
                GetQueryable().Where(entity =>
                    strs.Any(str => entity.Name.ToUpperInvariant().Contains(str)
                    || entity.Edrpou.ToUpperInvariant().Contains(str)
                    || entity.Country.Name.ToUpperInvariant().Contains(str)
                    || entity.Currency.ShortName.ToUpperInvariant().Contains(str)
                    || entity.Currency.FullName.ToUpperInvariant().Contains(str)))
                );

        protected override IQueryable<Supplier> ComplexEntities => Entities
                   .Include(t => t.Create)
                   .Include(z => z.Mod)
                   .Include(c => c.Currency)
                   .Include(c => c.Country)
                   .OrderByDescending(u => u.ModDate)
                   .ThenByDescending(x => x.CreateDate);
    }
}
