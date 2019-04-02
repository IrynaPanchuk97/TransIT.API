using System;
using System.Collections.Generic;
using System.Text;
using TransIT.DAL.Repositories.Interfaces;

namespace TransIT.DAL.UnitOfWork
{
    interface IUnitOfWork : IDisposable
    {
        IVehicleRepository Vehicles {get;}


        int Save();
    }
}
