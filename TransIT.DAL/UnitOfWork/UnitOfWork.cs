using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TransIT.DAL.Repositories.ImplementedRepositories;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public  IActionTypeRepository ActionTypeRepository { get; }
        public  ICountryRepository CountryRepository { get; }
        public  ICurrencyRepository CurrencyRepository { get; }
        public  IBillRepository BillRepository { get; }
        public  IDocumentRepository DocumentRepository { get; }
        public  IIssueRepository IssueRepository { get; }
        public  IIssueLogRepository IssueLogRepository { get; }
        public  IMalfunctionRepository MalfunctionRepository { get; }
        public  IMalfunctionGroupRepository MalfunctionGroupRepository { get; }
        public  IMalfunctionSubgroupRepository MalfunctionSubgroupRepository { get; }
        public  IRoleRepository RoleRepository { get; }
        public  IUserRepository UserRepository { get; }
        public  IStateRepository StateRepository { get; }
        public  ISupplierRepository SupplierRepository { get; }
        public  IVehicleRepository VehicleRepository { get; }
        public  IVehicleTypeRepository VehicleTypeRepository { get; }
        public  ITokenRepository TokenRepository { get; }


        public UnitOfWork(DbContext context,
            IActionTypeRepository actionTypeRepository,
            IBillRepository billRepository,
            ICurrencyRepository currencyRepository,
            ICountryRepository countryRepository,
            IDocumentRepository documentRepository,
            IIssueRepository issueRepository,
            IIssueLogRepository issueLogRepository,
            IMalfunctionRepository malfunctionRepository,
            IMalfunctionGroupRepository malfunctionGroupRepository,
            IMalfunctionSubgroupRepository malfunctionSubgroupRepository,
            IRoleRepository roleRepository,
            IStateRepository stateRepository,
            ISupplierRepository supplierRepository,
            IUserRepository userRepository,
            IVehicleRepository vehicleRepository,
            IVehicleTypeRepository vehicleTypeRepository,
            ITokenRepository tokenRepository)
        {
            _context = context;
            ActionTypeRepository = actionTypeRepository;
            BillRepository = billRepository;
            CountryRepository = countryRepository;
            CurrencyRepository = currencyRepository;
            DocumentRepository = documentRepository;
            IssueRepository = issueRepository;
            IssueLogRepository = issueLogRepository;
            MalfunctionRepository = malfunctionRepository;
            MalfunctionGroupRepository = malfunctionGroupRepository;
            MalfunctionSubgroupRepository = malfunctionSubgroupRepository;
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
