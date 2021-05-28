using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WizardGame.DataAccess;

namespace WizardGame.Api
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
            // For local database
            var connection = @"Server=(localdb)\MSSQLLocalDB;Database=Game.Database;Trusted_Connection=True;ConnectRetryCount=0";

            services.AddMvc(options =>
                options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddDbContext<GameContext>(options =>
                options.UseSqlServer(connection));
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // For Donau
            //SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            //{
            //    DataSource = @"donau.hiof.no",
            //    InitialCatalog = "lefaber",
            //    PersistSecurityInfo = true,
            //    UserID = "lefaber",
            //    Password = "tgJjs\"2d"
            //};
            //services.AddDbContext<GameContext>(options => options.UseSqlServer(builder.ConnectionString));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseMvc();
        }
    }
}
