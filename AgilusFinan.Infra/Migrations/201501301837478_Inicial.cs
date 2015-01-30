namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.Int(nullable: false),
                        Nome = c.String(),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresa", t => t.EmpresaId)
                .Index(t => t.EmpresaId);
            
            CreateTable(
                "dbo.Empresa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.Int(nullable: false),
                        EmailContato = c.String(),
                        EmailFinanceiro = c.String(),
                        Logotipo = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpresaId = c.Int(nullable: false),
                        Nome = c.String(),
                        Cpf = c.String(),
                        Rg = c.String(),
                        Endereco_Logradouro = c.String(),
                        Endereco_Numero = c.String(),
                        Endereco_Complemento = c.String(),
                        Endereco_Bairro = c.String(),
                        Endereco_Cidade = c.String(),
                        Endereco_Uf = c.String(),
                        Endereco_Cep = c.String(),
                        ContaBancaria_BancoId = c.Int(nullable: false),
                        ContaBancaria_Agencia = c.String(),
                        ContaBancaria_Numero = c.String(),
                        ContaBancaria_Poupanca = c.Boolean(nullable: false),
                        ContaBancaria_NomeTitular = c.String(),
                        ContaBancaria_CpfTitular = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresa", t => t.EmpresaId)
                .Index(t => t.EmpresaId);
            
            CreateTable(
                "dbo.TipoTelefone",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pessoa", "EmpresaId", "dbo.Empresa");
            DropForeignKey("dbo.Banco", "EmpresaId", "dbo.Empresa");
            DropIndex("dbo.Pessoa", new[] { "EmpresaId" });
            DropIndex("dbo.Banco", new[] { "EmpresaId" });
            DropTable("dbo.TipoTelefone");
            DropTable("dbo.Pessoa");
            DropTable("dbo.Empresa");
            DropTable("dbo.Banco");
        }
    }
}
