using Auditorias_Conector.DataAccess;
using Auditorias_Conector.Service;
using Microsoft.AspNetCore.Mvc;

namespace Auditorias_Conector.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuditoriaController : ControllerBase
    {
        private readonly AuditoriasService _auditoriasService;
        private readonly TeamplaceConnectorClient _teamplaceConnectorClient;

        public AuditoriaController(AuditoriasService auditoriasService, TeamplaceConnectorClient teamplaceConectorClient)
        {
            _auditoriasService = auditoriasService;
            _teamplaceConnectorClient = teamplaceConectorClient;
        }

        [HttpGet]
        [Route("Facturar")]

        public async Task<ActionResult> Auditoria()
        {
            try
            {
                await _auditoriasService.GetAuditoriaDAO();
                //await _teamplaceConnectorClient.PedidoVenta(jsonResult);
                //var nombre = await _teamplaceConnectorClient.NumeroTransaccion(jsonResult.IdentificacionExterna);

                //if (nombre != null)
                //{
                //    foreach (var item in nombre)
                //    {
                //        _auditoriasService.SaveNombre(item.Nombre, jsonResult.IdentificacionExterna);
                //    }
                //}

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
