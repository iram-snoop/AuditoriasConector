using Auditorias_Conector.DataAccess;
using Auditorias_Conector.Models.DTO;
using Auditorias_Conector.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public async Task<IActionResult> Auditoria()
        {
            var jsonResult = _auditoriasService.GetAuditoriaDAO();
            await _teamplaceConnectorClient.PedidoVenta(jsonResult);
            return Ok();
        }
    }
}
