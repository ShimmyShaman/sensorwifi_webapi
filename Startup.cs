using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi
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
            string dbName = Guid.NewGuid().ToString();
            // services.AddDbContext<TemperatureReportContext>(opt => opt.UseInMemoryDatabase(dbName), ServiceLifetime.Singleton, ServiceLifetime.Singleton);
            services.AddDbContext<TemperatureReportContext>(opt => opt.UseSqlServer("Server=tcp:tempwifi-server.database.windows.net,1433;Initial Catalog=tempwifidb;"
                + "Persist Security Info=False;User ID=tempwifiwa-admin;Password=twwa--123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;"
                + "Connection Timeout=30;"));
            services.AddControllers();

            // services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "webapi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Using swagger in live for the moment TODO
            // if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "webapi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
