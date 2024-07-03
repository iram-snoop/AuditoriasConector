using Auditorias_Conector.Interfaces;
using Auditorias_Conector.Models.DTO;
using Newtonsoft.Json;
using System.Collections.Generic;

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
                loggerService.Info($"Response StatusCode: {response.StatusCode}, Reason: {response.ReasonPhrase}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    loggerService.Error($"Error al crear pedido venta: {errorContent}");
                    throw new Exception($"Ocurrió un error al crear el pedido venta: {response.ReasonPhrase}");
                }
            }
            catch (Exception e)
            {
                loggerService.Error($"Exception: {e.Message}");
                throw new Exception(e.Message);
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
