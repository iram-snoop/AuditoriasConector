using Auditorias_Conector.DataAccess;
//using Hangfire;
using Microsoft.EntityFrameworkCore;
using Auditorias_Conector.Service;
using Auditorias_Conector.Interfaces;

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

            services.AddHttpClient("teamplace", client =>
            {
                client.BaseAddress = new Uri(Configuration["TeamplaceBaseUrl"]);
            });

            services.AddControllers();

            //services.AddHangfire(x =>
            //{
            //    x.UseSqlServerStorage(Configuration.GetConnectionString("AuditoriasHangfire"));
            //});

            //services.AddHangfireServer(options =>
            //{
            //    options.WorkerCount = 1; // Limitar la cantidad de trabajadores
            //});

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

            // Registro explícito del TeamplaceConnectorClient y AuditoriasService
            services.AddScoped<TeamplaceConnectorClient>();
            services.AddScoped<AuditoriasService>();

            // Registrar IHttpClientFactory
            services.AddHttpClient();

            // Registrar ILoggerService e AuditoriasAccess
            services.AddScoped<ILoggerService, LoggerService>(); // Reemplaza LoggerService con tu implementación concreta
            services.AddScoped<AuditoriasAccess>(); // Registrar AuditoriasAccess si es una clase concreta

            // Otros servicios necesarios
            services.ConfigureDbContexts(Configuration);
            services.ConfigureServices(Configuration);
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

            //app.UseHangfireDashboard("/hangfire", new DashboardOptions
            //{
            //    Authorization = Enumerable.Empty<Hangfire.Dashboard.IDashboardAuthorizationFilter>()
            //});

            //// Programar el trabajo recurrente con Hangfire
            //var interval = Configuration.GetValue<string>("JobSchedule:Interval");
            //RecurringJob.AddOrUpdate<AuditoriasService>("get-auditoria-dao", service => service.GetAuditoriaDAO(), Cron.MinuteInterval(30)); // Ejemplo: cada 30 minutos

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
