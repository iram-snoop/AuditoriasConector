using Auditorias_Conector.DataAccess;
using Auditorias_Conector.Interfaces;
using Auditorias_Conector.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Auditorias_Conector
{
    public static class StartupConfiguration
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfiguration configuration, string groupName)
        {
            var openApiInfo = new OpenApiInfo();
            configuration.GetSection("Application").Bind(openApiInfo);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auditorias Connector", Version = "v1" });
                // Otras configuraciones de Swagger
            });

            return services;
        }

        public static IServiceCollection ConfigureDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContextDB>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Auditorias"), sqlServerOptions => sqlServerOptions.CommandTimeout(90)),
                ServiceLifetime.Scoped);

            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditoriasService>();
            services.AddScoped<AuditoriasAccess>();


            // Registrar IHttpClientFactory
            services.AddHttpClient();

            // Registro de TeamplaceConnectorClient
            services.AddScoped<TeamplaceConnectorClient>();
            //services.AddSingleton<ILoggerService, LoggerService>();
            services.AddSingleton<LoggerService>();

            return services;
        }

        public static IServiceCollection AddConfig<T>(this IServiceCollection services, IConfiguration configuration, string sectionName = "") where T : class, new()
        {
            var config = string.IsNullOrEmpty(sectionName) ? configuration : configuration.GetSection(sectionName);
            var t = new T();
            config.Bind(t);
            services.AddSingleton(t);
            return services;
        }
    }
}
