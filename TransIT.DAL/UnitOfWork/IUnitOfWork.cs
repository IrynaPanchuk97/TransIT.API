using System;
using System.Collections.Generic;
using System.Text;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();
    }
}
