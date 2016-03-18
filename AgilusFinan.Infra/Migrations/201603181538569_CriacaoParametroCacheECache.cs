namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoParametroCacheECache : DbMigration
    {
        public override void Up()
        {
             CreateTable(
                "dbo.Cache",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 1000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);

         CreateTable(
                "dbo.ParametroCache",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CacheId = c.Int(nullable: false),
                        Nome = c.String(maxLength: 1000, unicode: false),
                        Valor = c.String(maxLength: 1000, unicode: false),
                    })
                .PrimaryKey(P => P.Id)
                .ForeignKey("dbo.Cache", t => t.CacheId, true, "FK_Id_CacheId")
                .Index(t => t.CacheId);
        }
        
        public override void Down()
        {
            DropTable("dbo.ParametroCache");
            DropTable("dbo.Cache");
        }
    }
}
