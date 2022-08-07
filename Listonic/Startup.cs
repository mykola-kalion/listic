using Listonic.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Listonic
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
            // services.AddControllers();
            //     .AddNewtonsoftJson(options =>
            // {
            //     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            // });

            // services.AddDbContext<ListonicDbContext>(options => { options.UseSqlite("Data Source=listonic_db.sqlite"); });

            // services.Configure<IdentityOptions>(options =>
            // {
            //     options.Password.RequireDigit = false;
            //     options.Password.RequiredLength = 5;
            //     options.Password.RequireLowercase = false;
            //     options.Password.RequireNonAlphanumeric = false;
            //     options.Password.RequireUppercase = false;
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }
            //
            // // app.UseHttpsRedirection();
            //
            // app.UseRouting();
            //
            // app.UseAuthentication();
            //
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}