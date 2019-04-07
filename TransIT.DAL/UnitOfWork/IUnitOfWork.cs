using System;
using System.Threading.Tasks;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.UnitOfWork
{
    public interface IUnitOfWork 
    {
        IActionTypeRepository ActionTypeRepository { get; }
        IBillRepository BillRepository { get; }
        IDocumentRepository DocumentRepository { get; }
        IIssueRepository IssueRepository { get; }
        IIssueLogRepository IssueLogRepository { get; }
        IMalfunctionRepository MalfunctionRepository { get; }
        IMalfunctionGroupRepository MalfunctionGroupRepository { get; }
        IMalfunctionSybgroupRepository MalfunctionSybgroupRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        IStateRepository StateRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        IVehicleRepository VehicleRepository { get; }
        IVehicleTypeRepository VehicleTypeRepository { get; }
        ITokenRepository TokenRepository { get; }

        Task<int> SaveAsync();
    }
}
