namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoContaTituloRecorrente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TituloRecorrente", "ContaId", c => c.Int(nullable: false));
            CreateIndex("dbo.TituloRecorrente", "ContaId");
            AddForeignKey("dbo.TituloRecorrente", "ContaId", "dbo.Conta", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TituloRecorrente", "ContaId", "dbo.Conta");
            DropIndex("dbo.TituloRecorrente", new[] { "ContaId" });
            DropColumn("dbo.TituloRecorrente", "ContaId");
        }
    }
}
