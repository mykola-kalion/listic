using Listonic.Extensions;
using Listonic.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Users.Extensions;

namespace API
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
            services.AddControllers();

            services.AddDbContext<ListonicDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("Default"));
                options.UseNpgsql(b => b.MigrationsAssembly("API"));
            });

            services.AddListonicServices();
            services.UserServices(Configuration);

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ListonicDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            dbContext.Database.Migrate();

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}