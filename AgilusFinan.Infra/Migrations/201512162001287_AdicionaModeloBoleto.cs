namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionaModeloBoleto : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Conta", name: "BancoBoletoId", newName: "BancoId");
            RenameIndex(table: "dbo.Conta", name: "IX_BancoBoletoId", newName: "IX_BancoId");
            CreateTable(
                "dbo.ModeloBoleto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContaId = c.Int(nullable: false),
                        Carteira = c.String(maxLength: 100, unicode: false),
                        Convenio = c.String(maxLength: 100, unicode: false),
                        CodigoCedente = c.String(maxLength: 100, unicode: false),
                        Juros = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Multa = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Instrucao = c.String(maxLength: 100, unicode: false),
                        NossoNumero = c.Int(nullable: false),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conta", t => t.ContaId)
                .ForeignKey("dbo.Empresa", t => t.EmpresaId)
                .Index(t => t.ContaId)
                .Index(t => t.EmpresaId);
            
            DropColumn("dbo.Conta", "Carteira");
            DropColumn("dbo.Conta", "Convenio");
            DropColumn("dbo.Conta", "CodigoCedente");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conta", "CodigoCedente", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.Conta", "Convenio", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.Conta", "Carteira", c => c.String(maxLength: 100, unicode: false));
            DropForeignKey("dbo.ModeloBoleto", "EmpresaId", "dbo.Empresa");
            DropForeignKey("dbo.ModeloBoleto", "ContaId", "dbo.Conta");
            DropIndex("dbo.ModeloBoleto", new[] { "EmpresaId" });
            DropIndex("dbo.ModeloBoleto", new[] { "ContaId" });
            DropTable("dbo.ModeloBoleto");
            RenameIndex(table: "dbo.Conta", name: "IX_BancoId", newName: "IX_BancoBoletoId");
            RenameColumn(table: "dbo.Conta", name: "BancoId", newName: "BancoBoletoId");
        }
    }
}
