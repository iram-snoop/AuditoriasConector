using Auditorias_Conector.DataAccess;
using Auditorias_Conector.Models.DTO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

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

        public void SaveNombre(string nombre, string identificacionExterna)
        {
            var idExt = Regex.Replace(identificacionExterna, @"[^\d]", "");
             _auditoriaAccess.SaveNombre(nombre, idExt);
        }
    }
}
