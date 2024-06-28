using Auditorias_Conector.DataAccess;
using Auditorias_Conector.Models.DAO;
using Auditorias_Conector.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Auditorias_Conector.Service
{
    public class AuditoriasService
    {
        private AuditoriasAccess _auditoriaAccess;

        public AuditoriasService(AuditoriasAccess auditoriasAccess)
        {
            _auditoriaAccess = auditoriasAccess;
        }

        public FacturaPedidoVentaDTO GetAuditoriaDAO()
        {
            var result = _auditoriaAccess.GetAuditoriaJson();
            return JsonConvert.DeserializeObject<FacturaPedidoVentaDTO>(result.JsonResult);
        }
    }
}
