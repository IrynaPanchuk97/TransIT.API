using AutoMapper;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
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
        
        private class RoleToStringConverter : ITypeConverter<Role, string>
        {
            public string Convert(Role source, string destination, ResolutionContext context) =>
                source.Name;
        }
        
        private class StringToRoleConverter : ITypeConverter<string, Role>
        {
            public Role Convert(string source, Role destination, ResolutionContext context) =>
                new Role {Name = source};
        }
    }
}