using Auditorias_Conector.DataAccess;
using Auditorias_Conector.Models.DTO;
using Auditorias_Conector.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Auditorias_Conector.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuditoriaController : ControllerBase
    {
        private readonly AuditoriasService _auditoriasService;
        private readonly TeamplaceConnectorClient _teamplaceConnectorClient;
        private readonly SipoCoreClient _sipoCoreClient;

        public AuditoriaController(AuditoriasService auditoriasService,
                                   TeamplaceConnectorClient teamplaceConectorClient,
                                   SipoCoreClient sipoCoreClient)
        {
            _auditoriasService = auditoriasService;
            _teamplaceConnectorClient = teamplaceConectorClient;
            _sipoCoreClient = sipoCoreClient;
        }

        [HttpGet]
        [Route("Facturar")]

        public async Task<ActionResult> Auditoria()
        {
            try
            {
                await _auditoriasService.GetAuditoriaDAO();

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        [Route("AddContact")]
        public async Task<ActionResult> AddContact()
        {
            try
            {
                var personas = await _auditoriasService.GetPersonasCore();
                await _sipoCoreClient.AddContact(1, personas, 6, null, null);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
