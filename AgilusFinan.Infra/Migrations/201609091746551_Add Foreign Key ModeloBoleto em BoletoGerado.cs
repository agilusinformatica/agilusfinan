namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyModeloBoletoemBoletoGerado : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.BoletoGerado", "ModeloBoletoId");
            AddForeignKey("dbo.BoletoGerado", "ModeloBoletoId", "dbo.ModeloBoleto", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BoletoGerado", "ModeloBoletoId", "dbo.ModeloBoleto");
            DropIndex("dbo.BoletoGerado", new[] { "ModeloBoletoId" });
        }
    }
}
