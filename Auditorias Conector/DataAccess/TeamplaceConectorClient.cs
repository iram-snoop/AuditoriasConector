using Auditorias_Conector.Models.DAO;
using Auditorias_Conector.Models.DTO;
using System.Net.Http;

namespace Auditorias_Conector.DataAccess
{
    public class TeamplaceConnectorClient
    {
        private HttpClient teamplaceClient = new();

        public TeamplaceConnectorClient(IHttpClientFactory clientFactory)
        {
            teamplaceClient = clientFactory.CreateClient("teamplace");
            teamplaceClient.BaseAddress = new Uri("https://localhost:51021"); // Configura la base address aquí
        }

        public async Task PedidoVenta(FacturaPedidoVentaDTO json)
        {
            try
            {
                var query = "/Actions/pedidoVenta";
                JsonContent content = JsonContent.Create(json);
                var response = await teamplaceClient.PostAsync(query, content);

                if (!response.IsSuccessStatusCode)
                {
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
