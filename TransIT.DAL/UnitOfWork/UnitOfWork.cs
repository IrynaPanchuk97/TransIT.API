using TransIT.DAL.Models;
using TransIT.DAL.Repositories;
using TransIT.DAL.Repositories.ImplementedRepositories;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.UnitOfWork
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext _context;
        public IVehicleRepository Vehicles { get; private set; }
        private readonly IVehicleRepository vehicleRepository;


        public UnitOfWork(DBContext context, ActionTypeRepository actionTypeRepository, BillRepository billRepository, DocumentRepository documentRepository, IssueRepository issueRepository, IssueLogRepository issueLogRepository, MalfunctionRepository malfunctionRepository, MalfunctionGroupRepository malfunctionGroupRepository, MalfunctionSubgroupRepository malfunctionSubgroupRepository, RoleRepository roleRepository, StateRepository stateRepository, SupplierRepository supplierRepository, UserRepository userRepository, VehicleRepository vehicleRepository, VehicleTypeRepository vehicleTypeRepository)
        {
            _context = context;
            this.vehicleRepository = vehicleRepository;
        }

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
