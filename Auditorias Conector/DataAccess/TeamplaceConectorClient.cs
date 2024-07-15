using Auditorias_Conector.Interfaces;
using Auditorias_Conector.Models.DTO;
using Newtonsoft.Json;

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

        public async Task<string> PedidoVenta(FacturaPedidoVentaDTO json)
        {
            try
            {
                var query = "/Actions/pedidoVenta";
                JsonContent content = JsonContent.Create(json);
                var response = await teamplaceClient.PostAsync(query, content);
                loggerService.Info($"Response StatusCode: {response.StatusCode}, Reason: {response.ReasonPhrase}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    loggerService.Error($"Error al crear pedido venta: {errorContent}");
                    return errorContent; // Devuelve el contenido del error
                }

                return string.Empty; // Devuelve una cadena vacía si no hay error
            }
            catch (Exception e)
            {
                loggerService.Error($"Excepción: {e.Message}");
                return e.Message; // Devuelve el mensaje de la excepción en lugar de lanzar una nueva excepción
            }
        }



        public async Task<List<GetNombreByIdExterna>> NumeroTransaccion(string identificadorExterno)
        {
            try
            {
                var query = "/Actions/NumeroTransaccion?identificadorExterno=" + identificadorExterno;

                var response = await teamplaceClient.GetAsync(query);
                string responseBody = await response.Content.ReadAsStringAsync();

                loggerService.Info(response + "response de GetAsync NumeroTransaccion al teamplace conector");
                if (!response.IsSuccessStatusCode)
                {
                    loggerService.Error(response + "error al obtener nombre de transaccion");
                    throw new Exception("Ocurrio un error al obtener nombre de transaccion");
                }

                var resultado = JsonConvert.DeserializeObject<List<GetNombreByIdExterna>>(responseBody);
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
