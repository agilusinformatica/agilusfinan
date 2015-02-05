using AgilusFinan.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AgilusFinan.Infra.Context
{
    public class Context : DbContext
    {
        public Context()
            : base("dbAgilusFinan")
        {

        }

        public DbSet<Banco> Bancos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<TipoTelefone> TiposTelefone { get; set; }
        public DbSet<TelefonePessoa> TelefonesPessoa { get; set; }
        public DbSet<TipoPessoa> TiposPessoa { get; set; }
        public DbSet<TipoPessoaPorPessoa> TiposPessoaPorPessoa { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
