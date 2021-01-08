using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Raise.MobileAppService.Repository;
using Raise.Repository;
using Raise.MobileAppService.Interfaces;
using Microsoft.EntityFrameworkCore;
using Raise.DataBase.Postgres;
using System;

namespace Raise.MobileAppService
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            PostgresContext.Configuration = Configuration;
            services.AddSingleton(PostgresContext.Configuration);

            services.AddEntityFrameworkNpgsql().AddDbContext<PostgresContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("RaiseDbConnection"), options => options.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(20), errorCodesToAdd: null)));

            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddScoped<IFeedRepository, FeedRepository>();

            services.AddScoped<IPostRepository, PostRepository>();

            services.AddApiVersioning(p =>
            {
                p.DefaultApiVersion = new ApiVersion(1, 0);
                p.ReportApiVersions = true;
                p.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Raise API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc()
                .UseApiVersioning()
                .UseMvcWithDefaultRoute();
            

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}