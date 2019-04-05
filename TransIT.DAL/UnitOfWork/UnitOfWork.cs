using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TransIT.DAL.Repositories.ImplementedRepositories;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public  IActionTypeRepository ActionTypeRepository;
        public  IBillRepository BillRepository;
        public  IDocumentRepository DocumentRepository;
        public  IIssueRepository IssueRepository;
        public  IIssueLogRepository IssueLogRepository;
        public  IMalfunctionRepository MalfunctionRepository;
        public  IMalfunctionGroupRepository MalfunctionGroupRepository;
        public  IMalfunctionSybgroupRepository MalfunctionSybgroupRepository;
        public  IRoleRepository RoleRepository;
        public  IUserRepository UserRepository;
        public  IStateRepository StateRepository;
        public  ISupplierRepository SupplierRepository;
        public  IVehicleRepository VehicleRepository;
        public  IVehicleTypeRepository VehicleTypeRepository;
        public  ITokenRepository TokenRepository;


        public UnitOfWork(DbContext context,
            ActionTypeRepository actionTypeRepository,
            BillRepository billRepository,
            DocumentRepository documentRepository,
            IssueRepository issueRepository,
            IssueLogRepository issueLogRepository,
            MalfunctionRepository malfunctionRepository,
            MalfunctionGroupRepository malfunctionGroupRepository,
            MalfunctionSubgroupRepository malfunctionSubgroupRepository,
            RoleRepository roleRepository,
            StateRepository stateRepository,
            SupplierRepository supplierRepository,
            UserRepository userRepository,
            VehicleRepository vehicleRepository,
            VehicleTypeRepository vehicleTypeRepository,
            TokenRepository tokenRepository)
        {
            _context = context;
            ActionTypeRepository = actionTypeRepository;
            BillRepository = billRepository;
            DocumentRepository = documentRepository;
            IssueRepository = issueRepository;
            IssueLogRepository = issueLogRepository;
            MalfunctionRepository = malfunctionRepository;
            MalfunctionGroupRepository = malfunctionGroupRepository;
            MalfunctionSybgroupRepository = malfunctionSubgroupRepository;
            RoleRepository = roleRepository;
            UserRepository = userRepository;
            StateRepository = stateRepository;
            SupplierRepository = supplierRepository;
            VehicleRepository = vehicleRepository;
            VehicleTypeRepository = vehicleTypeRepository;
            TokenRepository = tokenRepository;
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

    }
}
