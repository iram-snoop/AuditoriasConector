using Auditorias_Conector.DataAccess;

namespace Auditorias_Conector
{
    public class DependencyInjection
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _config;

        public DependencyInjection(IServiceCollection services, IConfiguration config)
        {
            _services = services;
            _config = config;

            InjectBusinessLogics();
            InjectClients();
        }

        private void InjectBusinessLogics()
        {
            _services.AddTransient<AuditoriaDAO>();
        }
        private void InjectClients()
        {
            _services.AddHttpClient("teamplace", c =>
            {
                c.BaseAddress = new Uri(_config["TeamplaceBaseUrl"]);
            });
            _services.AddTransient<TeamplaceConnectorClient>();
        }
    }
}
