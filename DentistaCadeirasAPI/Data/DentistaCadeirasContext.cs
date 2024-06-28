using DentistaCadeirasAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DentistaCadeirasAPI.Data
{
    public class DentistaCadeirasContext : DbContext
    {
        public DentistaCadeirasContext(DbContextOptions<DentistaCadeirasContext> options) : base(options) { }
        public DbSet<Cadeira> Cadeiras { get; set; }
        public DbSet<Alocacao> Alocacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cadeira>().HasData(
                new Cadeira { Id = 1, Numero = 1, Descricao = "Cadeira 1", Fabricante = "Fabricante A", UltimaManutencao = DateTime.Now, ProximaManutencao = DateTime.Now },
                new Cadeira { Id = 2, Numero = 2, Descricao = "Cadeira 2", Fabricante = "Fabricante A", UltimaManutencao = DateTime.Now, ProximaManutencao = DateTime.Now },
                new Cadeira { Id = 3, Numero = 3, Descricao = "Cadeira 3", Fabricante = "Fabricante B", UltimaManutencao = DateTime.Now, ProximaManutencao = DateTime.Now },
                new Cadeira { Id = 4, Numero = 4, Descricao = "Cadeira 4", Fabricante = "Fabricante B", UltimaManutencao = DateTime.Now, ProximaManutencao = DateTime.Now }
            );
        }
    }
}
