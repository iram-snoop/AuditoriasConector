using Auditorias_Conector.Service;
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

                    // Establecer el tiempo de espera del comando a 60 segundos (o cualquier valor deseado)
                    var commandTimeout = 60;

                    var items = connection.Query<AuditoriaDAO>("facturacionAuditorias",
                                transaction: _context.Database.CurrentTransaction?.GetDbTransaction(),
                                commandTimeout: commandTimeout, // Aquí se establece el tiempo de espera
                                commandType: CommandType.StoredProcedure);
                    return items.FirstOrDefault();
                }
            }
            catch (SqlException sqlEx)
            {
                // Registro de errores específicos de SQL
                throw new InvalidOperationException("Error de SQL al obtener el JSON de auditoría.", sqlEx);
            }
            catch (Exception ex)
            {
                // Registro de otros tipos de errores
                throw new InvalidOperationException("Ocurrió un error al obtener el JSON de auditoría.", ex);
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
