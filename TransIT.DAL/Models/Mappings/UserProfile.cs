using AutoMapper;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
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
            CreateMap<User, UserDTO>()
                .ForMember(u => u.Password, opt => opt.Ignore());
        }
    }
}
