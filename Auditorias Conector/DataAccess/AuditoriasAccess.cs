using Auditorias_Conector.Models.DAO;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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

        //public async Task<AuditoriaDAO> GetAuditoriaDAO()
        //{
        //    try
        //    {
        //        // Obtener la cadena JSON del procedimiento almacenado
        //        var jsonResult = GetAuditoriaJson();


        //        // Deserializar la cadena JSON a un objeto AuditoriaDAO
        //        //var auditoriaDAO = JsonConvert.DeserializeObject<AuditoriaDAO>(jsonResult);
        //        return auditoriaDAO;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new InvalidOperationException("Error al deserializar auditorías en formato JSON", ex);
        //    }
        //}


    }
}
