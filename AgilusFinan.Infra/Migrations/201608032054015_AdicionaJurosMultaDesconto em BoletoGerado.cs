namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionaJurosMultaDescontoemBoletoGerado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BoletoGerado", "PercentualDesconto", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.BoletoGerado", "Juros", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.BoletoGerado", "Multa", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.BoletoGerado", "DiasDesconto", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BoletoGerado", "DiasDesconto");
            DropColumn("dbo.BoletoGerado", "Multa");
            DropColumn("dbo.BoletoGerado", "Juros");
            DropColumn("dbo.BoletoGerado", "PercentualDesconto");
        }
    }
}
