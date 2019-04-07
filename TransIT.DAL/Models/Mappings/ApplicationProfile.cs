using System.Xml.Linq;
using AutoMapper;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.Mappings
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            MapAll();
        }

        private void MapAll()
        {
            MapRole();
            MapUser();
            MapToken();
            MapActionType();
            MapVehicleType();
            MapVehicle();
            MapMalfunctionGroup();
            MapMalfunctionSubgroup();
            MapMalfunction();
            MapState();
            MapIssue();
            MapIssueLog();
            MapDocument();
            MapBill();
            MapSupplier();
        }
        
        private void MapActionType()
        {
            CreateMap<ActionTypeDTO, ActionType>()
                .ForMember(a => a.ModId, opt => opt.Ignore())
                .ForMember(a => a.CreateId, opt => opt.Ignore())
                .ForMember(a => a.Mod, opt => opt.Ignore())
                .ForMember(a => a.Create, opt => opt.Ignore())
                .ForMember(a => a.ModDate, opt => opt.Ignore())
                .ForMember(a => a.CreateDate, opt => opt.Ignore())
                .ForMember(a => a.IssueLog, opt => opt.Ignore());
            CreateMap<ActionType, ActionTypeDTO>();
        }
        
        private void MapBill()
        {
            CreateMap<BillDTO, Bill>()
                .ForMember(b => b.ModId, opt => opt.Ignore())
                .ForMember(b => b.CreateId, opt => opt.Ignore())
                .ForMember(b => b.Mod, opt => opt.Ignore())
                .ForMember(b => b.Create, opt => opt.Ignore())
                .ForMember(b => b.DocumentId, opt => opt.Ignore())
                .ForMember(b => b.IssueId, opt => opt.Ignore());
            CreateMap<Bill, BillDTO>();
        }
        
        private void MapDocument()
        {
            CreateMap<DocumentDTO, Document>()
                .ForMember(d => d.ModId, opt => opt.Ignore())
                .ForMember(d => d.CreateId, opt => opt.Ignore())
                .ForMember(d => d.Mod, opt => opt.Ignore())
                .ForMember(d => d.Create, opt => opt.Ignore())
                .ForMember(d => d.Bill, opt => opt.Ignore())
                .ForMember(d => d.IssueLogId, opt => opt.Ignore());
            CreateMap<Document, DocumentDTO>();
        }

        private void MapIssue()
        {
            CreateMap<IssueDTO, Issue>()
                .ForMember(i => i.AssignedToNavigation, opt => opt.MapFrom(i => i.AssignedTo))
                .ForMember(i => i.ModId, opt => opt.Ignore())
                .ForMember(i => i.CreateId, opt => opt.Ignore())
                .ForMember(i => i.Mod, opt => opt.Ignore())
                .ForMember(i => i.Create, opt => opt.Ignore())
                .ForMember(i => i.Bill, opt => opt.Ignore())
                .ForMember(i => i.IssueLog, opt => opt.Ignore())
                .ForMember(i => i.StateId, opt => opt.Ignore())
                .ForMember(i => i.VehicleId, opt => opt.Ignore())
                .ForMember(i => i.MalfunctionId, opt => opt.Ignore())
                .ForMember(i => i.AssignedTo, opt => opt.Ignore());
            CreateMap<Issue, IssueDTO>();
        }

        private void MapIssueLog()
        {
            CreateMap<IssueLogDTO, IssueLog>()
                .ForMember(i => i.ModId, opt => opt.Ignore())
                .ForMember(i => i.CreateId, opt => opt.Ignore())
                .ForMember(i => i.Mod, opt => opt.Ignore())
                .ForMember(i => i.Create, opt => opt.Ignore())
                .ForMember(i => i.Document, opt => opt.Ignore())
                .ForMember(i => i.SupplierId, opt => opt.Ignore())
                .ForMember(i => i.NewStateId, opt => opt.Ignore())
                .ForMember(i => i.OldStateId, opt => opt.Ignore())
                .ForMember(i => i.ActionTypeId, opt => opt.Ignore());
            CreateMap<IssueLog, IssueLogDTO>();
        }
        
        private void MapMalfunctionGroup()
        {
            CreateMap<MalfunctionGroupDTO, MalfunctionGroup>()
                .ForMember(m => m.ModId, opt => opt.Ignore())
                .ForMember(m => m.CreateId, opt => opt.Ignore())
                .ForMember(m => m.Mod, opt => opt.Ignore())
                .ForMember(m => m.Create, opt => opt.Ignore())
                .ForMember(m => m.ModDate, opt => opt.Ignore())
                .ForMember(m => m.CreateDate, opt => opt.Ignore());
            CreateMap<MalfunctionGroup, MalfunctionGroupDTO>();
        }
        
        private void MapMalfunctionSubgroup()
        {
            CreateMap<MalfunctionSubgroupDTO, MalfunctionSubgroup>()
                .ForMember(m => m.ModId, opt => opt.Ignore())
                .ForMember(m => m.CreateId, opt => opt.Ignore())
                .ForMember(m => m.Mod, opt => opt.Ignore())
                .ForMember(m => m.Create, opt => opt.Ignore())
                .ForMember(m => m.ModDate, opt => opt.Ignore())
                .ForMember(m => m.CreateDate, opt => opt.Ignore())
                .ForMember(m => m.MalfunctionGroupId, opt => opt.Ignore());
            CreateMap<MalfunctionSubgroup, MalfunctionSubgroupDTO>();
        }
        
        private void MapMalfunction()
        {
            CreateMap<MalfunctionDTO, Malfunction>()
                .ForMember(m => m.ModId, opt => opt.Ignore())
                .ForMember(m => m.CreateId, opt => opt.Ignore())
                .ForMember(m => m.Mod, opt => opt.Ignore())
                .ForMember(m => m.Create, opt => opt.Ignore())
                .ForMember(m => m.ModDate, opt => opt.Ignore())
                .ForMember(m => m.CreateDate, opt => opt.Ignore())
                .ForMember(m => m.MalfunctionSubgroupId, opt => opt.Ignore());
            CreateMap<Malfunction, MalfunctionDTO>();
        }
        
        private void MapState()
        {
            CreateMap<StateDTO, State>()
                .ForMember(s => s.Issue, opt => opt.Ignore())
                .ForMember(s => s.IssueLogNewState, opt => opt.Ignore())
                .ForMember(s => s.IssueLogOldState, opt => opt.Ignore());
            CreateMap<State, StateDTO>();
        }
        
        private void MapSupplier()
        {
            CreateMap<SupplierDTO, Supplier>()
                .ForMember(s => s.ModDate, opt => opt.Ignore())
                .ForMember(s => s.CreateDate, opt => opt.Ignore())
                .ForMember(s => s.ModId, opt => opt.Ignore())
                .ForMember(s => s.CreateId, opt => opt.Ignore())
                .ForMember(s => s.Mod, opt => opt.Ignore())
                .ForMember(s => s.Create, opt => opt.Ignore())
                .ForMember(s => s.IssueLog, opt => opt.Ignore());
            CreateMap<Supplier, SupplierDTO>();
        }
        
        private void MapToken()
        {
            CreateMap<TokenDTO, Token>()
                .ForMember(t => t.ModId, opt => opt.Ignore())
                .ForMember(t => t.CreateId, opt => opt.Ignore())
                .ForMember(t => t.Mod, opt => opt.Ignore())
                .ForMember(t => t.Create, opt => opt.Ignore())
                .ForMember(t => t.ModDate, opt => opt.Ignore())
                .ForMember(t => t.CreateDate, opt => opt.Ignore())
                .ForMember(t => t.RefreshToken, opt => opt.Ignore());
            CreateMap<Token, TokenDTO>()
                .ForMember(t => t.AccessToken, opt => opt.Ignore());
        }

        private void MapRole()
        {
            CreateMap<Role, string>()
                .ConvertUsing<RoleToStringConverter>();
            CreateMap<string, Role>()
                .ForMember(t => t.ModId, opt => opt.Ignore())
                .ForMember(t => t.CreateId, opt => opt.Ignore())
                .ForMember(t => t.Mod, opt => opt.Ignore())
                .ForMember(t => t.Create, opt => opt.Ignore())
                .ForMember(t => t.ModDate, opt => opt.Ignore())
                .ForMember(t => t.CreateDate, opt => opt.Ignore())
                .ForMember(t => t.User, opt => opt.Ignore())

                .ConvertUsing<StringToRoleConverter>();
        }
        
        private void MapUser()
        {
            CreateMap<UserDTO, User>()
                .ForMember(u => u.ModId, opt => opt.Ignore())
                .ForMember(u => u.CreateId, opt => opt.Ignore())
                .ForMember(u => u.Mod, opt => opt.Ignore())
                .ForMember(u => u.Create, opt => opt.Ignore())
                .ForMember(u => u.ModDate, opt => opt.Ignore())
                .ForMember(u => u.CreateDate, opt => opt.Ignore())
                .ForMember(u => u.RoleId, opt => opt.Ignore())

                .ForMember(u => u.IssueAssignedToNavigation, opt => opt.Ignore())

                .ForMember(u => u.BillMod, opt => opt.Ignore())
                .ForMember(u => u.MalfunctionGroupMod, opt => opt.Ignore())
                .ForMember(u => u.MalfunctionSubgroupMod, opt => opt.Ignore())
                .ForMember(u => u.MalfunctionMod, opt => opt.Ignore())
                .ForMember(u => u.RoleMod, opt => opt.Ignore())
                .ForMember(u => u.IssueMod, opt => opt.Ignore())
                .ForMember(u => u.IssueLogMod, opt => opt.Ignore())
                .ForMember(u => u.TokenMod, opt => opt.Ignore())
                .ForMember(u => u.DocumentMod, opt => opt.Ignore())
                .ForMember(u => u.VehicleMod, opt => opt.Ignore())
                .ForMember(u => u.VehicleTypeMod, opt => opt.Ignore())
                .ForMember(u => u.ActionTypeMod, opt => opt.Ignore())
                .ForMember(u => u.InverseMod, opt => opt.Ignore())
                .ForMember(u => u.SupplierMod, opt => opt.Ignore())

                .ForMember(u => u.BillCreate, opt => opt.Ignore())
                .ForMember(u => u.MalfunctionGroupCreate, opt => opt.Ignore())
                .ForMember(u => u.MalfunctionSubgroupCreate, opt => opt.Ignore())
                .ForMember(u => u.MalfunctionCreate, opt => opt.Ignore())
                .ForMember(u => u.RoleCreate, opt => opt.Ignore())
                .ForMember(u => u.IssueCreate, opt => opt.Ignore())
                .ForMember(u => u.IssueLogCreate, opt => opt.Ignore())
                .ForMember(u => u.TokenCreate, opt => opt.Ignore())
                .ForMember(u => u.DocumentCreate, opt => opt.Ignore())
                .ForMember(u => u.VehicleCreate, opt => opt.Ignore())
                .ForMember(u => u.VehicleTypeCreate, opt => opt.Ignore())
                .ForMember(u => u.ActionTypeCreate, opt => opt.Ignore())
                .ForMember(u => u.InverseCreate, opt => opt.Ignore())
                .ForMember(u => u.SupplierCreate, opt => opt.Ignore());
            CreateMap<User, UserDTO>();
        }
        
        private void MapVehicleType()
        {
            CreateMap<VehicleTypeDTO, VehicleType>()
                .ForMember(v => v.ModId, opt => opt.Ignore())
                .ForMember(v => v.CreateId, opt => opt.Ignore())
                .ForMember(v => v.Mod, opt => opt.Ignore())
                .ForMember(v => v.Create, opt => opt.Ignore())
                .ForMember(v => v.Vehicle, opt => opt.Ignore())
                .ForMember(v => v.ModDate, opt => opt.Ignore())
                .ForMember(v => v.CreateDate, opt => opt.Ignore());
            CreateMap<VehicleType, VehicleTypeDTO>();
        }
        
        private void MapVehicle()
        {
            CreateMap<VehicleDTO, Vehicle>()
                .ForMember(v => v.ModId, opt => opt.Ignore())
                .ForMember(v => v.CreateId, opt => opt.Ignore())
                .ForMember(v => v.Mod, opt => opt.Ignore())
                .ForMember(v => v.Create, opt => opt.Ignore())
                .ForMember(v => v.Issue, opt => opt.Ignore())
                .ForMember(v => v.InventoryId, opt => opt.Ignore())
                .ForMember(v => v.VehicleTypeId, opt => opt.Ignore())
                .ForMember(v => v.ModDate, opt => opt.Ignore())
                .ForMember(v => v.CreateDate, opt => opt.Ignore());
            CreateMap<Vehicle, VehicleDTO>();
        }
        
        class RoleToStringConverter : ITypeConverter<Role, string>
        {
            public string Convert(Role source, string destination, ResolutionContext context) =>
                source.Name;
        }
        
        class StringToRoleConverter : ITypeConverter<string, Role>
        {
            public Role Convert(string source, Role destination, ResolutionContext context) =>
                new Role {Name = source};
        }
    }
}