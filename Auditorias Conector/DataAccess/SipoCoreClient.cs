using Auditorias_Conector.Interfaces;
using Auditorias_Conector.Models.DTO;

namespace Auditorias_Conector.DataAccess
{
    public class SipoCoreClient
    {
        private HttpClient coreClient = new();
        private readonly ILoggerService loggerService;

        public SipoCoreClient(IHttpClientFactory clientFactory, ILoggerService loggerService)
        {
            coreClient = clientFactory.CreateClient("Core");
            this.loggerService = loggerService;
        }

        public async Task<string> AddContact(int id, IEnumerable<PersonaBasicaDTO> personas, int? tipoSistema = null, int? idArea = null, int? idContacto = null)
        {
            try
            {
                string query = $"/api/empresa/{id}/contactos";
                var queryParams = new List<string>
        {
            $"id={id}",
            $"tipoSistema={(tipoSistema.HasValue ? tipoSistema.ToString() : "null")}",
            $"idArea={(idArea.HasValue ? idArea.ToString() : "null")}",
            $"idContacto={(idContacto.HasValue ? idContacto.ToString() : "null")}"
        };

                query += "?" + string.Join("&", queryParams);

                JsonContent content = JsonContent.Create(personas);

                var response = await coreClient.PostAsync(query, content);

                loggerService.Info($"Response StatusCode: {response.StatusCode}, Reason: {response.ReasonPhrase}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    loggerService.Error($"Error al añadir contactos: {errorContent}");
                    return errorContent;
                }

                return string.Empty;
            }
            catch (Exception e)
            {
                loggerService.Error($"Excepción: {e.Message}");
                return e.Message;
            }
        }




    }
}
