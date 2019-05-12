using AutoMapper;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.Mappings
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<CountryDTO, Country>()
                .ForMember(m => m.ModId, opt => opt.Ignore())
                .ForMember(m => m.CreateId, opt => opt.Ignore());
            CreateMap<Country, CountryDTO>();
        }
    }
}