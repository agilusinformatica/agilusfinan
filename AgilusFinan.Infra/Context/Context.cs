using AgilusFinan.Domain.Entities;
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
        public DbSet<EsquecimentoSenha> EsquecimentosSenha { get; set; }
        public DbSet<FeriadoRegional> FeriadosRegionais { get; set; }
        public DbSet<FeriadoGeral> FeriadosGerais { get; set; }
        public DbSet<ModeloBoleto> ModelosBoleto { get; set; }
        public DbSet<BoletoGerado> BoletosGerado { get; set; }
        public DbSet<Parametro> Parametros { get; set; }

        public int EmpresaId
        {
            get
            {
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
            modelBuilder.Entity<Acesso>().HasRequired(t => t.Perfil).WithMany(t => t.Acessos).HasForeignKey(d => d.PerfilId).WillCascadeOnDelete(true);
            modelBuilder.Entity<Titulo>().Ignore(d => d.Liquidado);
            modelBuilder.Entity<Usuario>().Property(u => u.Email).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.Senha).IsRequired();
            modelBuilder.Entity<Titulo>().Property(p => p.Observacao).HasMaxLength(500);
            modelBuilder.Entity<Pessoa>().Property(p => p.EmailFinanceiro).HasMaxLength(500);
            modelBuilder.Entity<Convite>().HasRequired(t => t.Perfil).WithMany(t => t.Convites).HasForeignKey(d => d.PerfilId).WillCascadeOnDelete(true);
            modelBuilder.Entity<ModeloBoleto>().Property(p => p.TextoEmail).HasColumnType("varchar(max)"); //É necessário colocar senão o entity não entende que a coluna sofreu alteração.
            modelBuilder.Entity<ModeloBoleto>().Property(p => p.TextoEmail).IsMaxLength(); //Passa o tamanho máximo ao provider, se usar o maxLength vc é obrigado a passar um tamanho int, se passa null ele assume o valor configurado por default, que nesse caso é 100
        }
    }
}
