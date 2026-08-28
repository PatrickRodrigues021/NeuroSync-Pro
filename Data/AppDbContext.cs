using Microsoft.EntityFrameworkCore;
using NeuroSync.Models;

namespace NeuroSync.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Prontuario> Prontuarios { get; set; }
        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }
        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Cria o banco SQLite chamado "neurosync.db" na raiz do projeto
            optionsBuilder.UseSqlite("Data Source=neurosync.db");
        }
    }
}