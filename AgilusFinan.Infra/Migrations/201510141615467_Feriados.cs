namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Feriados : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FeriadoGeral",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 100, unicode: false),
                        Data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(p => p.Data, unique: true);
            
            CreateTable(
                "dbo.FeriadoRegional",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 100, unicode: false),
                        Data = c.DateTime(nullable: false),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresa", t => t.EmpresaId)
                .Index(t => t.EmpresaId)
                .Index(p => p.Data, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FeriadoRegional", "EmpresaId", "dbo.Empresa");
            DropIndex("dbo.FeriadoRegional", new[] { "EmpresaId" });
            DropTable("dbo.FeriadoRegional");
            DropTable("dbo.FeriadoGeral");
        }
    }
}
