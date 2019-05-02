using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TransIT.BLL.Services;
using TransIT.BLL.Services.ImplementedServices;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Models.Mappings;
using TransIT.DAL.Repositories;
using TransIT.DAL.Repositories.ImplementedRepositories;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.API.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(new MapperConfiguration(c =>
            {
                c.AddProfile(new RoleProfile());
                c.AddProfile(new UserProfile());
                c.AddProfile(new TokenProfile());
                c.AddProfile(new VehicleTypeProfile());
                c.AddProfile(new VehicleProfile());
                c.AddProfile(new RoleProfile());
                c.AddProfile(new MalfunctionGroupProfile());
                c.AddProfile(new MalfunctionSubgroupProfile());
                c.AddProfile(new MalfunctionProfile());
                c.AddProfile(new StateProfile());
                c.AddProfile(new ActionTypeProfile());
                c.AddProfile(new IssueProfile());
                c.AddProfile(new IssueLogProfile());
                c.AddProfile(new DocumentProfile());
                c.AddProfile(new SupplierProfile());
            }).CreateMapper());
        }

        public static void ConfigureDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IActionTypeService, ActionTypeService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IVehicleTypeService, VehicleTypeService>();
            services.AddScoped<IMalfunctionService, MalfunctionService>();
            services.AddScoped<IMalfunctionGroupService, MalfunctionGroupService>();
            services.AddScoped<IMalfunctionSubgroupService, MalfunctionSubgroupService>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IIssueService, IssueService>();
            services.AddScoped<IIssueLogService, IssueLogService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IStateService, StateService>();
            
            services.AddScoped<IODCrudService<User>, ODCrudService<User>>();
            services.AddScoped<IODCrudService<ActionType>, ODCrudService<ActionType>>();
            services.AddScoped<IODCrudService<Vehicle>, ODCrudService<Vehicle>>();
            services.AddScoped<IODCrudService<VehicleType>, ODCrudService<VehicleType>>();
            services.AddScoped<IODCrudService<Malfunction>, ODCrudService<Malfunction>>();
            services.AddScoped<IODCrudService<MalfunctionGroup>, ODCrudService<MalfunctionGroup>>();
            services.AddScoped<IODCrudService<MalfunctionSubgroup>, ODCrudService<MalfunctionSubgroup>>();
            services.AddScoped<IODCrudService<Bill>, ODCrudService<Bill>>();
            services.AddScoped<IODCrudService<Document>, ODCrudService<Document>>();
            services.AddScoped<IODCrudService<Issue>, ODCrudService<Issue>>();
            services.AddScoped<IODCrudService<IssueLog>, ODCrudService<IssueLog>>();
            services.AddScoped<IODCrudService<Supplier>, ODCrudService<Supplier>>();
            services.AddScoped<IODCrudService<Role>, ODCrudService<Role>>();
            services.AddScoped<IODCrudService<State>, ODCrudService<State>>();
        }

        public static void ConfigureModelRepositories(this IServiceCollection services)
        {
            services.AddScoped<IActionTypeRepository, ActionTypeRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IIssueRepository, IssueRepository>();
            services.AddScoped<IIssueLogRepository, IssueLogRepository>();
            services.AddScoped<IMalfunctionRepository, MalfunctionRepository>();
            services.AddScoped<IMalfunctionGroupRepository, MalfunctionGroupRepository>();
            services.AddScoped<IMalfunctionSubgroupRepository, MalfunctionSubgroupRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
            
            services.AddScoped<IODRepository<ActionType>, ActionTypeRepository>();
            services.AddScoped<IODRepository<Bill>, BillRepository>();
            services.AddScoped<IODRepository<Document>, DocumentRepository>();
            services.AddScoped<IODRepository<Issue>, IssueRepository>();
            services.AddScoped<IODRepository<IssueLog>, IssueLogRepository>();
            services.AddScoped<IODRepository<Malfunction>, MalfunctionRepository>();
            services.AddScoped<IODRepository<MalfunctionGroup>, MalfunctionGroupRepository>();
            services.AddScoped<IODRepository<MalfunctionSubgroup>, MalfunctionSubgroupRepository>();
            services.AddScoped<IODRepository<Role>, RoleRepository>();
            services.AddScoped<IODRepository<State>, StateRepository>();
            services.AddScoped<IODRepository<Supplier>, SupplierRepository>();
            services.AddScoped<IODRepository<Token>, TokenRepository>();
            services.AddScoped<IODRepository<User>, UserRepository>();
            services.AddScoped<IODRepository<Vehicle>, VehicleRepository>();
            services.AddScoped<IODRepository<VehicleType>, VehicleTypeRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
