using System;
using System.Threading.Tasks;

namespace TransIT.DAL.UnitOfWork
{
    public interface IUnitOfWork 
    {
        Task<int> SaveAsync();
    }
}
