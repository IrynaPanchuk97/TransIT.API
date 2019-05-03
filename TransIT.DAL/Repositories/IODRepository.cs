using System.Linq;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Repositories
{
    public interface IODRepository<TEntity>
    {
        IQueryable<TEntity> GetQueryable();
    }
}
