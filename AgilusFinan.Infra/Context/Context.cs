using AgilusFinan.Domain.Entities;
//using AgilusFinan.Infra.EntityConfig;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace AgilusFinan.Infra.Context
{
    public class Contexto : DbContext
    {
        public Contexto()
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
        public DbSet<CentroCusto> CentrosCusto { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Titulo> Titulos { get; set; }
        public DbSet<TituloRecorrente> TitulosRecorrentes { get; set; }
        public DbSet<Transferencia> Transferencias { get; set; }
        public DbSet<Liquidacao> Liquidacoes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Acesso> Acessos { get; set; }
        public DbSet<Funcao> Funcoes { get; set; }
        public DbSet<Convite> Convites { get; set; }

        public int EmpresaId
        {
            get
            {
                //return this.Database.SqlQuery<int>("select top 1 Id from Empresa order by Id").First<int>();
                return UsuarioLogado.EmpresaId;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(100));
            modelBuilder.Entity<Categoria>().Property(p => p.CategoriaPaiId).IsOptional();
            modelBuilder.Entity<TipoPessoaPorPessoa>().HasRequired(t => t.Pessoa).WithMany(t => t.TiposPessoa).HasForeignKey(d => d.PessoaId).WillCascadeOnDelete(true);
            modelBuilder.Entity<TelefonePessoa>().HasRequired(t => t.Pessoa).WithMany(t => t.Telefones).HasForeignKey(d => d.PessoaId).WillCascadeOnDelete(true);
            modelBuilder.Entity<Titulo>().Ignore(d => d.Liquidado);
            modelBuilder.Entity<Usuario>().Property(u => u.Email).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.Senha).IsRequired();
        }
    }
}
