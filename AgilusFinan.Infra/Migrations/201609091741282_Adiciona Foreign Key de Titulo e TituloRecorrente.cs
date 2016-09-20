namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionaForeignKeydeTituloeTituloRecorrente : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.BoletoGerado", "TituloId");
            CreateIndex("dbo.BoletoGerado", "TituloRecorrenteId");
            AddForeignKey("dbo.BoletoGerado", "TituloId", "dbo.Titulo", "Id");
            AddForeignKey("dbo.BoletoGerado", "TituloRecorrenteId", "dbo.TituloRecorrente", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BoletoGerado", "TituloRecorrenteId", "dbo.TituloRecorrente");
            DropForeignKey("dbo.BoletoGerado", "TituloId", "dbo.Titulo");
            DropIndex("dbo.BoletoGerado", new[] { "TituloRecorrenteId" });
            DropIndex("dbo.BoletoGerado", new[] { "TituloId" });
        }
    }
}
