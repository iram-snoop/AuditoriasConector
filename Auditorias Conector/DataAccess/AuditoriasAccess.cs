using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using System.Data;

namespace Auditorias_Conector.DataAccess
{
    public class AuditoriasAccess : DBAccess<AuditoriaDAO>
    {
        private IOptions<MyConfig> config;
        private HttpClient teamplaceClient;
        private readonly IConfiguration _configuration;

        public AuditoriasAccess(IOptions<MyConfig> config, ContextDB contextDB, IConfiguration configuration) : base(contextDB)
        {
            this.config = config;
            teamplaceClient = new HttpClient();
            _configuration = configuration;
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
                using (var connection = new SqlConnection(_configuration.GetConnectionString("Auditorias")))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    // Set the command timeout to 60 seconds (or any desired value)
                    var commandTimeout = 120;

                    var items = connection.Query<AuditoriaDAO>("facturacionAuditorias",
                                transaction: _context.Database.CurrentTransaction?.GetDbTransaction(),
                                commandTimeout: commandTimeout, // Here we set the timeout
                                commandType: CommandType.StoredProcedure);

                    return items.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while getting auditoria json.", ex);
            }
        }


        public void SaveNombre(string nombre, int identificacionExterna)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("Auditorias");

                using (var connection = new SqlConnection(connectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    var parameters = new
                    {
                        IdentificacionExterna = identificacionExterna,
                        Nombre = nombre
                    };

                    connection.Execute(
                        "AddNombreEnOrdenFacturacionByIdExt",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocurrió un error al guardar nombre en orden de facturación.";
                errorMessage += $"\nTipo de excepción: {ex.GetType().FullName}";
                errorMessage += $"\nMensaje: {ex.Message}";
                errorMessage += $"\nStack Trace: {ex.StackTrace}";

                throw new InvalidOperationException(errorMessage, ex);
            }
        }

        public void SaveError(string error, int identificacionExterna)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("Auditorias");

                using (var connection = new SqlConnection(connectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    var parameters = new
                    {
                        IdentificacionExterna = identificacionExterna,
                        Error = error
                    };

                    connection.Execute(
                        "ConcatenarError",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocurrió un error al guardar error en orden de facturación.";
                errorMessage += $"\nTipo de excepción: {ex.GetType().FullName}";
                errorMessage += $"\nMensaje: {ex.Message}";
                errorMessage += $"\nStack Trace: {ex.StackTrace}";

                throw new InvalidOperationException(errorMessage, ex);
            }
        }
    }
}
