using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransIT.API.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.EntityFrameworkCore;
using TransIT.BLL.Security.Hashers;
using TransIT.DAL.Models;
using TransIT.API.Hubs;

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
            services.AddDbContext<DbContext, TransITDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AzureConnection"));
            });

            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            
            services.AddSignalR();
            services.ConfigureAutoMapper();
            services.ConfigureAuthentication(Configuration);
            services.ConfigureCors();
            services.ConfigureModelRepositories();
            services.ConfigureDataAccessServices();
            services.AddOData();

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
            app.UseAuthentication();
            app.UseCors("CorsPolicy");
            app.UseMvc(routerBuilder =>
            {
                routerBuilder.EnableDependencyInjection();
                routerBuilder.Count().OrderBy().Filter().MaxTop(1000);
            });
            app.UseSignalR(routes =>
            {
                routes.MapHub<IssueHub>("/issuehub");
            });
        }
    }
}
