using Microsoft.OpenApi.Models;

namespace Auditorias_Conector
{
    public static class StartupConfiguration
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services,
                IConfiguration configuration,
                string groupName)
        {
            var openApiInfo = new OpenApiInfo();
            configuration.GetSection("Application").Bind(openApiInfo);
            var swaggerVersions = new HashSet<string>();

            services.AddSwaggerGen(c =>
            {
                var existing = c.SwaggerGeneratorOptions.SwaggerDocs.ContainsKey("v1");
                if (!existing)
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auditorias Connector", Version = "v1" });
                }
                // Otras configuraciones de Swagger
            });


            return services;
        }

        //public static IServiceCollection ConfigureDbContexts(this IServiceCollection services, IConfiguration Configuration)
        //{
        //    //services.AddDbContext<ContextDB>(options =>
        //    //    options.UseSqlServer(Configuration.GetConnectionString("TeamplaceCache"), sqlServerOptions => sqlServerOptions.CommandTimeout(90)),
        //    //    ServiceLifetime.Scoped);

        //    //return services;
        //}

        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            //services.AddScoped<UnitOfWork>();
            //services.AddSingleton<IConfigurationManager, ConfiguracionManager>();

            return services;
        }

        public static IServiceCollection AddConfig<T>(
                    this IServiceCollection services,
                    IConfiguration configuration, string sectionName = "") where T : class, new()
        {
            var config = string.IsNullOrEmpty(sectionName) ? configuration : configuration.GetSection(sectionName);
            var t = new T();
            config.Bind(t);
            services.AddSingleton(t);
            return services;
        }
    }
}
