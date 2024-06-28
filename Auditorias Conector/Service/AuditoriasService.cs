using Auditorias_Conector.DataAccess;
using Auditorias_Conector.Models.DAO;

namespace Auditorias_Conector.Service
{
    public class AuditoriasService
    {
        private AuditoriasAccess _auditoriaAccess;

        public AuditoriasService(AuditoriasAccess auditoriasAccess)
        {
            _auditoriaAccess = auditoriasAccess;
        }

        public AuditoriaDAO GetAuditoriaDAO()
        {
            var result = _auditoriaAccess.GetAuditoriaJson();
            return result;
        }
    }
}
