namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriaTabelaParametroseCriacampoemPessoa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Parametro",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TextoEmailLiquidacao = c.String(maxLength: 100, unicode: false),
                        AssuntoEmailLiquidacao = c.String(maxLength: 100, unicode: false),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresa", t => t.EmpresaId)
                .Index(t => t.EmpresaId);
            
            AddColumn("dbo.Pessoa", "RecebeEmailLiquidacao", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Parametro", "EmpresaId", "dbo.Empresa");
            DropIndex("dbo.Parametro", new[] { "EmpresaId" });
            DropColumn("dbo.Pessoa", "RecebeEmailLiquidacao");
            DropTable("dbo.Parametro");
        }
    }
}
