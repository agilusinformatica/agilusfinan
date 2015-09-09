namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoEntidadeConvite : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Convite",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 100, unicode: false),
                        PerfilId = c.Int(nullable: false),
                        Expirado = c.Boolean(nullable: false),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresa", t => t.EmpresaId)
                .ForeignKey("dbo.Perfil", t => t.PerfilId)
                .Index(t => t.PerfilId)
                .Index(t => t.EmpresaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Convite", "PerfilId", "dbo.Perfil");
            DropForeignKey("dbo.Convite", "EmpresaId", "dbo.Empresa");
            DropIndex("dbo.Convite", new[] { "EmpresaId" });
            DropIndex("dbo.Convite", new[] { "PerfilId" });
            DropTable("dbo.Convite");
        }
    }
}
