namespace Auditorias_Conector.Interfaces
{
    public interface ILoggerService
    {
        public void Info(string msg);

        public void Error(string msg);

        public void Error(Exception ex, string msg);

        public void Warn(string msg);
    }
}