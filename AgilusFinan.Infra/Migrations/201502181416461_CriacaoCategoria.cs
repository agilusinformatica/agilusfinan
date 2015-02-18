namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoCategoria : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Direcao = c.Int(nullable: false),
                        Cor = c.Int(nullable: false),
                        CategoriaPaiId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categoria", t => t.CategoriaPaiId)
                .Index(t => t.CategoriaPaiId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categoria", "CategoriaPaiId", "dbo.Categoria");
            DropIndex("dbo.Categoria", new[] { "CategoriaPaiId" });
            DropTable("dbo.Categoria");
        }
    }
}
