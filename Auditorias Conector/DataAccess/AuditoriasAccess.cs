using Dapper;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using System.Data;

namespace Auditorias_Conector.DataAccess
{
    public class AuditoriasAccess : DBAccess<AuditoriaDAO>
    {
        private IOptions<MyConfig> config;
        private HttpClient teamplaceClient;

        public AuditoriasAccess(IOptions<MyConfig> config, ContextDB contextDB) : base(contextDB)
        {
            this.config = config;
            teamplaceClient = new HttpClient();
        }

        public AuditoriasAccess(ContextDB contextDB) : base(contextDB)
        {
            this.config = config;
            teamplaceClient = new HttpClient();
        }

        public AuditoriaDAO GetAuditoriaJson()
        {
            try
            {
                using (var connection = _context.Connection)
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    var items = connection.Query<AuditoriaDAO>("facturacionAuditorias",
                                transaction: _context.Database.CurrentTransaction?.GetDbTransaction(),
                                commandType: CommandType.StoredProcedure);

                    return items.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while getting auditoria json.", ex);
            }
        }
    }
}
