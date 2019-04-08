using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TransIT.DAL.Repositories.ImplementedRepositories;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public IActionTypeRepository ActionTypeRepository { get; }
        public  IBillRepository BillRepository { get; }
        public  IDocumentRepository DocumentRepository { get; }
        public  IIssueRepository IssueRepository { get; }
        public  IIssueLogRepository IssueLogRepository { get; }
        public  IMalfunctionRepository MalfunctionRepository { get; }
        public  IMalfunctionGroupRepository MalfunctionGroupRepository { get; }
        public  IMalfunctionSubgroupRepository MalfunctionSybgroupRepository { get; }
        public  IRoleRepository RoleRepository { get; }
        public  IUserRepository UserRepository { get; }
        public  IStateRepository StateRepository { get; }
        public  ISupplierRepository SupplierRepository { get; }
        public  IVehicleRepository VehicleRepository { get; }
        public  IVehicleTypeRepository VehicleTypeRepository { get; }
        public  ITokenRepository TokenRepository { get; }


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
