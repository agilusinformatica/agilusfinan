namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoTitulo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pagamento", "CategoriaId", "dbo.Categoria");
            DropForeignKey("dbo.Pagamento", "CentroCustoId", "dbo.CentroCusto");
            DropForeignKey("dbo.Pagamento", "ContaId", "dbo.Conta");
            DropForeignKey("dbo.Pagamento", "EmpresaId", "dbo.Empresa");
            DropForeignKey("dbo.Pagamento", "PessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Recebimento", "CategoriaId", "dbo.Categoria");
            DropForeignKey("dbo.Recebimento", "CentroCustoId", "dbo.CentroCusto");
            DropForeignKey("dbo.Recebimento", "ContaId", "dbo.Conta");
            DropForeignKey("dbo.Recebimento", "EmpresaId", "dbo.Empresa");
            DropForeignKey("dbo.Recebimento", "PessoaId", "dbo.Pessoa");
            DropIndex("dbo.Pagamento", new[] { "ContaId" });
            DropIndex("dbo.Pagamento", new[] { "CategoriaId" });
            DropIndex("dbo.Pagamento", new[] { "PessoaId" });
            DropIndex("dbo.Pagamento", new[] { "CentroCustoId" });
            DropIndex("dbo.Pagamento", new[] { "EmpresaId" });
            DropIndex("dbo.Recebimento", new[] { "ContaId" });
            DropIndex("dbo.Recebimento", new[] { "CategoriaId" });
            DropIndex("dbo.Recebimento", new[] { "PessoaId" });
            DropIndex("dbo.Recebimento", new[] { "CentroCustoId" });
            DropIndex("dbo.Recebimento", new[] { "EmpresaId" });
            CreateTable(
                "dbo.Titulo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContaId = c.Int(nullable: false),
                        DataVencimento = c.DateTime(nullable: false),
                        Descricao = c.String(maxLength: 100, unicode: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoriaId = c.Int(nullable: false),
                        PessoaId = c.Int(nullable: false),
                        CentroCustoId = c.Int(),
                        Competencia = c.DateTime(),
                        Observacao = c.String(maxLength: 100, unicode: false),
                        TituloRecorrenteId = c.Int(),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categoria", t => t.CategoriaId)
                .ForeignKey("dbo.CentroCusto", t => t.CentroCustoId)
                .ForeignKey("dbo.Conta", t => t.ContaId)
                .ForeignKey("dbo.Empresa", t => t.EmpresaId)
                .ForeignKey("dbo.Pessoa", t => t.PessoaId)
                .ForeignKey("dbo.TituloRecorrente", t => t.TituloRecorrenteId)
                .Index(t => t.ContaId)
                .Index(t => t.CategoriaId)
                .Index(t => t.PessoaId)
                .Index(t => t.CentroCustoId)
                .Index(t => t.TituloRecorrenteId)
                .Index(t => t.EmpresaId);
            
            CreateTable(
                "dbo.Liquidacao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        JurosMulta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FormaLiquidacao = c.Int(nullable: false),
                        TituloId = c.Int(nullable: false),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresa", t => t.EmpresaId)
                .ForeignKey("dbo.Titulo", t => t.TituloId)
                .Index(t => t.TituloId)
                .Index(t => t.EmpresaId);
            
            CreateTable(
                "dbo.TituloRecorrente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 100, unicode: false),
                        DiaVencimento = c.Int(nullable: false),
                        Valor = c.Decimal(precision: 18, scale: 2),
                        Recorrencia = c.Int(nullable: false),
                        QtdeParcelas = c.Int(),
                        Ativo = c.Boolean(nullable: false),
                        PessoaId = c.Int(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                        CentroCustoId = c.Int(),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categoria", t => t.CategoriaId)
                .ForeignKey("dbo.CentroCusto", t => t.CentroCustoId)
                .ForeignKey("dbo.Empresa", t => t.EmpresaId)
                .ForeignKey("dbo.Pessoa", t => t.PessoaId)
                .Index(t => t.PessoaId)
                .Index(t => t.CategoriaId)
                .Index(t => t.CentroCustoId)
                .Index(t => t.EmpresaId);
            
            DropTable("dbo.Pagamento");
            DropTable("dbo.Recebimento");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Recebimento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContaId = c.Int(nullable: false),
                        DataVencimento = c.DateTime(nullable: false),
                        DataLiquidacao = c.DateTime(nullable: false),
                        ValorLiquidado = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Descricao = c.String(maxLength: 100, unicode: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoriaId = c.Int(nullable: false),
                        PessoaId = c.Int(nullable: false),
                        CentroCustoId = c.Int(nullable: false),
                        Competencia = c.DateTime(nullable: false),
                        FormaPagamento = c.Int(nullable: false),
                        JurosMulta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Observacao = c.String(maxLength: 100, unicode: false),
                        Recorrencia_Periodo = c.Int(nullable: false),
                        Recorrencia_QuantidadeParcelas = c.Int(nullable: false),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pagamento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContaId = c.Int(nullable: false),
                        DataVencimento = c.DateTime(nullable: false),
                        DataLiquidacao = c.DateTime(nullable: false),
                        ValorLiquidado = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Descricao = c.String(maxLength: 100, unicode: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoriaId = c.Int(nullable: false),
                        PessoaId = c.Int(nullable: false),
                        CentroCustoId = c.Int(nullable: false),
                        Competencia = c.DateTime(nullable: false),
                        FormaPagamento = c.Int(nullable: false),
                        JurosMulta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Observacao = c.String(maxLength: 100, unicode: false),
                        Recorrencia_Periodo = c.Int(nullable: false),
                        Recorrencia_QuantidadeParcelas = c.Int(nullable: false),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Titulo", "TituloRecorrenteId", "dbo.TituloRecorrente");
            DropForeignKey("dbo.TituloRecorrente", "PessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.TituloRecorrente", "EmpresaId", "dbo.Empresa");
            DropForeignKey("dbo.TituloRecorrente", "CentroCustoId", "dbo.CentroCusto");
            DropForeignKey("dbo.TituloRecorrente", "CategoriaId", "dbo.Categoria");
            DropForeignKey("dbo.Titulo", "PessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Liquidacao", "TituloId", "dbo.Titulo");
            DropForeignKey("dbo.Liquidacao", "EmpresaId", "dbo.Empresa");
            DropForeignKey("dbo.Titulo", "EmpresaId", "dbo.Empresa");
            DropForeignKey("dbo.Titulo", "ContaId", "dbo.Conta");
            DropForeignKey("dbo.Titulo", "CentroCustoId", "dbo.CentroCusto");
            DropForeignKey("dbo.Titulo", "CategoriaId", "dbo.Categoria");
            DropIndex("dbo.TituloRecorrente", new[] { "EmpresaId" });
            DropIndex("dbo.TituloRecorrente", new[] { "CentroCustoId" });
            DropIndex("dbo.TituloRecorrente", new[] { "CategoriaId" });
            DropIndex("dbo.TituloRecorrente", new[] { "PessoaId" });
            DropIndex("dbo.Liquidacao", new[] { "EmpresaId" });
            DropIndex("dbo.Liquidacao", new[] { "TituloId" });
            DropIndex("dbo.Titulo", new[] { "EmpresaId" });
            DropIndex("dbo.Titulo", new[] { "TituloRecorrenteId" });
            DropIndex("dbo.Titulo", new[] { "CentroCustoId" });
            DropIndex("dbo.Titulo", new[] { "PessoaId" });
            DropIndex("dbo.Titulo", new[] { "CategoriaId" });
            DropIndex("dbo.Titulo", new[] { "ContaId" });
            DropTable("dbo.TituloRecorrente");
            DropTable("dbo.Liquidacao");
            DropTable("dbo.Titulo");
            CreateIndex("dbo.Recebimento", "EmpresaId");
            CreateIndex("dbo.Recebimento", "CentroCustoId");
            CreateIndex("dbo.Recebimento", "PessoaId");
            CreateIndex("dbo.Recebimento", "CategoriaId");
            CreateIndex("dbo.Recebimento", "ContaId");
            CreateIndex("dbo.Pagamento", "EmpresaId");
            CreateIndex("dbo.Pagamento", "CentroCustoId");
            CreateIndex("dbo.Pagamento", "PessoaId");
            CreateIndex("dbo.Pagamento", "CategoriaId");
            CreateIndex("dbo.Pagamento", "ContaId");
            AddForeignKey("dbo.Recebimento", "PessoaId", "dbo.Pessoa", "Id");
            AddForeignKey("dbo.Recebimento", "EmpresaId", "dbo.Empresa", "Id");
            AddForeignKey("dbo.Recebimento", "ContaId", "dbo.Conta", "Id");
            AddForeignKey("dbo.Recebimento", "CentroCustoId", "dbo.CentroCusto", "Id");
            AddForeignKey("dbo.Recebimento", "CategoriaId", "dbo.Categoria", "Id");
            AddForeignKey("dbo.Pagamento", "PessoaId", "dbo.Pessoa", "Id");
            AddForeignKey("dbo.Pagamento", "EmpresaId", "dbo.Empresa", "Id");
            AddForeignKey("dbo.Pagamento", "ContaId", "dbo.Conta", "Id");
            AddForeignKey("dbo.Pagamento", "CentroCustoId", "dbo.CentroCusto", "Id");
            AddForeignKey("dbo.Pagamento", "CategoriaId", "dbo.Categoria", "Id");
        }
    }
}
