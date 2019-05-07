using System.Linq;

namespace TransIT.DAL.Repositories
{
    public interface IQueryRepository<TEntity>
    {
        IQueryable<TEntity> GetQueryable();
    }
}
