using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TransIT.DAL.Repositories.ImplementedRepositories;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        private readonly IActionTypeRepository _actionTypeRepository;
        private readonly IBillRepository _billRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IIssueRepository _issueRepository;
        private readonly IIssueLogRepository _issueLogRepository;
        private readonly IMalfunctionRepository _malfunctionRepository;
        private readonly IMalfunctionGroupRepository _malfunctionGroupRepository;
        private readonly IMalfunctionSybgroupRepository _malfunctionSybgroupRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStateRepository _stateRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehicleTypeRepository _vehicleTypeRepository;
        private readonly ITokenRepository _tokenRepository;


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
            _actionTypeRepository = actionTypeRepository;
            _billRepository = billRepository;
            _documentRepository = documentRepository;
            _issueRepository = issueRepository;
            _issueLogRepository = issueLogRepository;
            _malfunctionRepository = malfunctionRepository;
            _malfunctionGroupRepository = malfunctionGroupRepository;
            _malfunctionSybgroupRepository = malfunctionSubgroupRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _stateRepository = stateRepository;
            _supplierRepository = supplierRepository;
            _vehicleRepository = vehicleRepository;
            _vehicleTypeRepository = vehicleTypeRepository;
            _tokenRepository = tokenRepository;
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

    }
}
