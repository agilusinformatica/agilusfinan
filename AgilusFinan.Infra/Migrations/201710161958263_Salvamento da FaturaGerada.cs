namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalvamentodaFaturaGerada : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FaturaGerada",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IuguId = c.String(maxLength: 100, unicode: false),
                        DataVencimento = c.DateTime(nullable: false),
                        UrlFatura = c.String(maxLength: 100, unicode: false),
                        TituloId = c.Int(),
                        TituloRecorrenteId = c.Int(),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresa", t => t.EmpresaId)
                .Index(t => t.EmpresaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FaturaGerada", "EmpresaId", "dbo.Empresa");
            DropIndex("dbo.FaturaGerada", new[] { "EmpresaId" });
            DropTable("dbo.FaturaGerada");
        }
    }
}
