namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaodasclassesPagamentoeRecebimento : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pagamento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpresaId = c.Int(nullable: false),
                        ContaId = c.Int(nullable: false),
                        DataVencimento = c.DateTime(nullable: false),
                        Descricao = c.String(),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataPagamento = c.DateTime(nullable: false),
                        ValorPago = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoriaId = c.Int(nullable: false),
                        PessoaId = c.Int(nullable: false),
                        CentroCustoId = c.Int(nullable: false),
                        Competencia = c.DateTime(nullable: false),
                        FormaPagamento = c.Int(nullable: false),
                        JurosMulta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Observacao = c.String(),
                        Recorrencia_Periodo = c.Int(nullable: false),
                        Recorrencia_QuantidadeParcelas = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categoria", t => t.CategoriaId)
                .ForeignKey("dbo.CentroCusto", t => t.CentroCustoId)
                .ForeignKey("dbo.Conta", t => t.ContaId)
                .ForeignKey("dbo.Empresa", t => t.EmpresaId)
                .ForeignKey("dbo.Pessoa", t => t.PessoaId)
                .Index(t => t.EmpresaId)
                .Index(t => t.ContaId)
                .Index(t => t.CategoriaId)
                .Index(t => t.PessoaId)
                .Index(t => t.CentroCustoId);
            
            CreateTable(
                "dbo.Recebimento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpresaId = c.Int(nullable: false),
                        ContaId = c.Int(nullable: false),
                        DataVencimento = c.DateTime(nullable: false),
                        Descricao = c.String(),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataRecebimento = c.DateTime(nullable: false),
                        ValorRecebido = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoriaId = c.Int(nullable: false),
                        PessoaId = c.Int(nullable: false),
                        CentroCustoId = c.Int(nullable: false),
                        Competencia = c.DateTime(nullable: false),
                        FormaPagamento = c.Int(nullable: false),
                        JurosMulta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Observacao = c.String(),
                        Recorrencia_Periodo = c.Int(nullable: false),
                        Recorrencia_QuantidadeParcelas = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categoria", t => t.CategoriaId)
                .ForeignKey("dbo.CentroCusto", t => t.CentroCustoId)
                .ForeignKey("dbo.Conta", t => t.ContaId)
                .ForeignKey("dbo.Empresa", t => t.EmpresaId)
                .ForeignKey("dbo.Pessoa", t => t.PessoaId)
                .Index(t => t.EmpresaId)
                .Index(t => t.ContaId)
                .Index(t => t.CategoriaId)
                .Index(t => t.PessoaId)
                .Index(t => t.CentroCustoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recebimento", "PessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Recebimento", "EmpresaId", "dbo.Empresa");
            DropForeignKey("dbo.Recebimento", "ContaId", "dbo.Conta");
            DropForeignKey("dbo.Recebimento", "CentroCustoId", "dbo.CentroCusto");
            DropForeignKey("dbo.Recebimento", "CategoriaId", "dbo.Categoria");
            DropForeignKey("dbo.Pagamento", "PessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Pagamento", "EmpresaId", "dbo.Empresa");
            DropForeignKey("dbo.Pagamento", "ContaId", "dbo.Conta");
            DropForeignKey("dbo.Pagamento", "CentroCustoId", "dbo.CentroCusto");
            DropForeignKey("dbo.Pagamento", "CategoriaId", "dbo.Categoria");
            DropIndex("dbo.Recebimento", new[] { "CentroCustoId" });
            DropIndex("dbo.Recebimento", new[] { "PessoaId" });
            DropIndex("dbo.Recebimento", new[] { "CategoriaId" });
            DropIndex("dbo.Recebimento", new[] { "ContaId" });
            DropIndex("dbo.Recebimento", new[] { "EmpresaId" });
            DropIndex("dbo.Pagamento", new[] { "CentroCustoId" });
            DropIndex("dbo.Pagamento", new[] { "PessoaId" });
            DropIndex("dbo.Pagamento", new[] { "CategoriaId" });
            DropIndex("dbo.Pagamento", new[] { "ContaId" });
            DropIndex("dbo.Pagamento", new[] { "EmpresaId" });
            DropTable("dbo.Recebimento");
            DropTable("dbo.Pagamento");
        }
    }
}
