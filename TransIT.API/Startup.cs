using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TransIT.API.Extensions;
using TransIT.BLL.Services;
using FluentValidation.AspNetCore;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.Repositories.ImplementedRepositories;
using TransIT.DAL.UnitOfWork;
using TransIT.BLL.Security.Hashers;
using TransIT.DAL.Models;

namespace TransIT.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureAutoMapper();

            #region Services

            services.AddScoped<ICrudService<User>, UserService>();

            #endregion


            #region            
            services.AddScoped<IActionTypeRepository, ActionTypeRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IIssueRepository, IssueRepository>();
            services.AddScoped<IIssueLogRepository, IssueLogRepository>();
            services.AddScoped<IMalfunctionRepository, MalfunctionRepository>();
            services.AddScoped<IMalfunctionGroupRepository, MalfunctionGroupRepository>();
            services.AddScoped<IMalfunctionSybgroupRepository, MalfunctionSubgroupRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<TransITDBContext>();

            services.AddSingleton<IPasswordHasher>();
            #endregion
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
