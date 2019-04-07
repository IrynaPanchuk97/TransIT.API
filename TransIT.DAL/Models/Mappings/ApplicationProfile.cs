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
            CreateMap<ActionTypeDTO, ActionType>();
            CreateMap<ActionType, ActionTypeDTO>();
        }
        
        private void MapBill()
        {
            CreateMap<BillDTO, Bill>();
            CreateMap<Bill, BillDTO>();
        }
        
        private void MapDocument()
        {
            CreateMap<DocumentDTO, Document>();
            CreateMap<Document, DocumentDTO>();
        }

        private void MapIssue()
        {
            CreateMap<IssueDTO, Issue>();
            CreateMap<Issue, IssueDTO>();
        }

        private void MapIssueLog()
        {
            CreateMap<IssueLogDTO, IssueLog>();
            CreateMap<IssueLog, IssueLogDTO>();
        }
        
        private void MapMalfunctionGroup()
        {
            CreateMap<MalfunctionGroupDTO, MalfunctionGroup>();
            CreateMap<MalfunctionGroup, MalfunctionGroupDTO>();
        }
        
        private void MapMalfunctionSubgroup()
        {
            CreateMap<MalfunctionSubgroupDTO, MalfunctionSubgroup>();
            CreateMap<MalfunctionSubgroup, MalfunctionSubgroupDTO>();
        }
        
        private void MapMalfunction()
        {
            CreateMap<MalfunctionDTO, Malfunction>();
            CreateMap<Malfunction, MalfunctionDTO>();
        }
        
        private void MapState()
        {
            CreateMap<StateDTO, State>();
            CreateMap<State, StateDTO>();
        }
        
        private void MapSupplier()
        {
            CreateMap<SupplierDTO, Supplier>();
            CreateMap<Supplier, SupplierDTO>();
        }
        
        private void MapToken()
        {
            CreateMap<TokenDTO, Token>();
            CreateMap<Token, TokenDTO>();
        }
        
        private void MapUser()
        {
            CreateMap<UserDTO, User>()
                .ForMember(u => u.Role, opt => opt.NullSubstitute(new Role()))
                .ForMember(u => u.Role.Name, map => map.MapFrom(m => m.Role));
            CreateMap<User, UserDTO>()
                .ForMember(u => u.Role, map => map.MapFrom(m => m.Role.Name));
        }
        
        private void MapVehicleType()
        {
            CreateMap<VehicleTypeDTO, VehicleType>();
            CreateMap<VehicleType, VehicleTypeDTO>();
        }
        
        private void MapVehicle()
        {
            CreateMap<VehicleDTO, Vehicle>();
            CreateMap<Vehicle, VehicleDTO>();
        }
    }
}