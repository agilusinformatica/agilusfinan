namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposPercentualDescontoModeloBoleto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ModeloBoleto", "DiasDesconto", c => c.Int(nullable: false));
            AddColumn("dbo.ModeloBoleto", "PercentualDesconto", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ModeloBoleto", "PercentualDesconto");
            DropColumn("dbo.ModeloBoleto", "DiasDesconto");
        }
    }
}
