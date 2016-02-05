namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionaBoletoGerado : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoletoGerado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TituloId = c.Int(),
                        TituloRecorrenteId = c.Int(),
                        ModeloBoletoId = c.Int(nullable: false),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresa", t => t.EmpresaId)
                .Index(t => t.EmpresaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BoletoGerado", "EmpresaId", "dbo.Empresa");
            DropIndex("dbo.BoletoGerado", new[] { "EmpresaId" });
            DropTable("dbo.BoletoGerado");
        }
    }
}
