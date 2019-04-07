using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TransIT.DAL.Models.Mappings;

namespace TransIT.API.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(new MapperConfiguration(c =>
            {
                c.AddProfile(new ApplicationProfile());
            }).CreateMapper());
        }
    }
}