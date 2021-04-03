using Database.DatabaseModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApi
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
            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                    builder =>
                    {
                        //builder.WithOrigins("*").SetIsOriginAllowedToAllowWildcardSubdomains();
                        //builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                        builder.WithOrigins("https://pchealth.azurewebsites.net",
                                "http://localhost:3000")
                            .WithMethods("GET");
                    });
            });
            string mySqlConnectionStr = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<PcHealthContext>(options => options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr)));


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
