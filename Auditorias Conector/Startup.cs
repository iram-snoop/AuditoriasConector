using Auditorias_Conector.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net.Http;

namespace Auditorias_Conector
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ContextDB>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("Auditorias")));

            services.AddControllers();

            // Configuración de Swagger
            services.ConfigureSwagger(Configuration, "v1");

            services.AddCors(options =>
            {
                options.AddPolicy(name: "snoopCors",
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin()
                                             .AllowAnyMethod()
                                             .AllowAnyHeader();
                                  });
            });

            // Registrar IHttpClientFactory
            services.AddHttpClient();

            // Registrar servicios adicionales
            services.ConfigureDbContexts(Configuration);
            services.ConfigureServices(Configuration);

            // Registro explícito del TeamplaceConnectorClient
            services.AddScoped<TeamplaceConnectorClient>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("snoopCors");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // Habilitar Swagger y Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auditorias Connector v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
