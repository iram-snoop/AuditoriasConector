using Auditorias_Conector.DataAccess;
using Auditorias_Conector.Models.DTO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Auditorias_Conector.Service
{
    public class AuditoriasService
    {
        private AuditoriasAccess _auditoriaAccess;
        private readonly TeamplaceConnectorClient _teamplaceConnectorClient;

        public AuditoriasService(AuditoriasAccess auditoriasAccess, TeamplaceConnectorClient teamplaceConnectorClient)
        {
            _auditoriaAccess = auditoriasAccess;
            _teamplaceConnectorClient = teamplaceConnectorClient;
        }

        public async Task GetAuditoriaDAO()
        {
            FacturaPedidoVentaDTO jsonResult;

            do
            {
                var result = _auditoriaAccess.GetAuditoriaJson();

                if (result == null || string.IsNullOrEmpty(result.JsonResult))
                {
                    jsonResult = null;
                }
                else
                {
                    jsonResult = JsonConvert.DeserializeObject<FacturaPedidoVentaDTO>(result.JsonResult);

                    var ifResultadoIsError = await _teamplaceConnectorClient.PedidoVenta(jsonResult);
                    var nombre = await _teamplaceConnectorClient.NumeroTransaccion(jsonResult.IdentificacionExterna);

                    if (!String.IsNullOrEmpty(ifResultadoIsError))
                    {
                        SaveError(ifResultadoIsError, jsonResult.IdentificacionExterna);
                    }

                    if (nombre != null)
                    {
                        foreach (var item in nombre)
                        {
                            SaveNombre(item.Nombre, jsonResult.IdentificacionExterna);
                        }
                    }
                }

            } while (jsonResult != null);
        }

        public void SaveNombre(string nombre, string identificacionExterna)
        {
            var idExt = Regex.Replace(identificacionExterna, @"[^\d]", "");
            int idExtInt = int.Parse(idExt);

            _auditoriaAccess.SaveNombre(nombre, idExtInt);
        }

        public void SaveError(string error, string identificacionExterna)
        {
            var idExt = Regex.Replace(identificacionExterna, @"[^\d]", "");
            int idExtInt = int.Parse(idExt);

            _auditoriaAccess.SaveError(error, idExtInt);
        }
    }
}
