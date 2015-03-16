namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullCategoriaPaiId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Categoria", new[] { "CategoriaPaiId" });
            AlterColumn("dbo.Categoria", "CategoriaPaiId", c => c.Int());
            CreateIndex("dbo.Categoria", "CategoriaPaiId");
            Sql("update dbo.Categoria set CategoriaPaiId = null where Id = CategoriaPaiId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Categoria", new[] { "CategoriaPaiId" });
            Sql("update dbo.Categoria set CategoriaPaiId = Id where CategoriaPaiId is null");
            AlterColumn("dbo.Categoria", "CategoriaPaiId", c => c.Int(nullable: false));
            CreateIndex("dbo.Categoria", "CategoriaPaiId");
        }
    }
}
