using Auditorias_Conector.Interfaces;
using Serilog;

namespace Auditorias_Conector.Service
{
    public class LoggerService : ILoggerService
    {
        private readonly Serilog.ILogger log;

        public LoggerService(IConfiguration configuration)
        {
            string path = configuration.GetSection("Logging")["LogFile"].ToString();
            log = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .WriteTo.File(path)
                .CreateLogger();
        }

        public void Info(string msg)
        {
            log.Information(msg);
        }

        public void Error(string msg)
        {
            log.Error(msg);
        }

        public void Error(Exception ex, string msg)
        {
            log.Error(ex, msg);
        }

        public void Warn(string msg)
        {
            log.Warning(msg);
        }
    }
}