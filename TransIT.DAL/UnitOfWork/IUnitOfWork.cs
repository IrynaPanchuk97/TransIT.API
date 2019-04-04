using System;
using System.Threading.Tasks;

namespace TransIT.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Save();
    }
}
