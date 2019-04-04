using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransIT.DAL.Repositories.ImplementedRepositories;
using TransIT.DAL.Repositories.InterfacesRepositories;

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


            //services.AddScoped<IActionTypeRepository, ActionTypeRepository>();
            //services.AddScoped<IBillRepository, BillRepository>();
            //services.AddScoped<IDocumentRepository, DocumentRepository>();
            //services.AddScoped<IIssueLogRepository, IssueLogRepository>();
            //services.AddScoped<IIssueRepository, IIssueRepository>();
            //services.AddScoped<IMalfunctionRepository, MalfunctionRepository>();
            //services.AddScoped<IMalfunctionGroupRepository, MalfunctionGroupRepository>();
            //services.AddScoped<IMalfunctionSybgroupRepository, MalfunctionSubgroupRepository>();
            //services.AddScoped<IRoleRepository, RoleRepository>();
            //services.AddScoped<IStateRepository, StateRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<ISupplierRepository, SupplierRepository>();
            //services.AddScoped<IVehicleRepository, VehicleRepository>();
            //services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
      
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
