using TransIT.DAL.Models;
using TransIT.DAL.Repositories;
using TransIT.DAL.Repositories.Interfaces;

namespace TransIT.DAL.UnitOfWork
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly TransITDBContext _context;

        public UnitOfWork(TransITDBContext context)
        {
            _context = context;
            Vehicles = new VehicleRepository(_context);
        }

        public IVehicleRepository Vehicles { get; private set; }


        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
