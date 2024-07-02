using Auditorias_Conector.Interfaces;
using Auditorias_Conector.Models.DTO;

namespace Auditorias_Conector.DataAccess
{
    public class TeamplaceConnectorClient
    {
        private HttpClient teamplaceClient = new();
        private readonly ILoggerService loggerService;

        public TeamplaceConnectorClient(IHttpClientFactory clientFactory, ILoggerService loggerService)
        {
            teamplaceClient = clientFactory.CreateClient("teamplace");
            this.loggerService = loggerService;
        }

        public async Task PedidoVenta(FacturaPedidoVentaDTO json)
        {
            try
            {
                var query = "/Actions/pedidoVenta";
                JsonContent content = JsonContent.Create(json);
                var response = await teamplaceClient.PostAsync(query, content);
                loggerService.Info(response + "response de PostAsync al teamplace conector");
                if (!response.IsSuccessStatusCode)
                {
                    loggerService.Error(response + "error al crear pedido venta");
                    throw new Exception("Ocurrio un error al crear el pedido venta");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
