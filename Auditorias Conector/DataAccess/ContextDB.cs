using Auditorias_Conector.Models.DAO;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Auditorias_Conector.DataAccess
{
    public class ContextDB : DbContext
    {
        public ContextDB()
        {
        }

        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {
        }

        public IDbConnection Connection => Database.GetDbConnection();

        public DbSet<AuditoriaDAO> Auditorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditoriaDAO>()
                    .HasNoKey();

            modelBuilder.Entity<Filial>()
                    .HasNoKey();

            modelBuilder.Entity<Empresa>()
                    .HasNoKey();

            modelBuilder.Entity<Persona>()
                    .HasNoKey();

            modelBuilder.Entity<CategoriaFiscal>()
                    .HasNoKey();

            modelBuilder.Entity<MetodoPago>()
                    .HasNoKey();

            modelBuilder.Entity<OrdenFacturacion>()
                    .HasNoKey();

            modelBuilder.Entity<Curso>()
                    .HasNoKey();

            modelBuilder.Entity<Participante>()
                    .HasNoKey();

            modelBuilder.Entity<Producto>()
                    .HasNoKey();
        }
    }
}
