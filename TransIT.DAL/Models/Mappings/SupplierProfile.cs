using AutoMapper;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.Mappings
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<SupplierDTO, Supplier>()
                .ForMember(s => s.CountryId, opt => opt.MapFrom(x => x.Country.Id))
                .ForMember(s => s.CurrencyId, opt => opt.MapFrom(x => x.Currency.Id))
                .ForMember(s => s.Country, opt => opt.Ignore())
                .ForMember(s => s.Currency, opt => opt.Ignore())
                .ForMember(s => s.ModId, opt => opt.Ignore())
                .ForMember(s => s.CreateId, opt => opt.Ignore())
                .ForMember(s => s.IssueLog, opt => opt.Ignore());
            CreateMap<Supplier, SupplierDTO>();
        }
    }
}
