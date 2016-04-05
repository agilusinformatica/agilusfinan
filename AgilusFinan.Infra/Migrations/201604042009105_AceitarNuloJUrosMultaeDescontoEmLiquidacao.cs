namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AceitarNuloJUrosMultaeDescontoEmLiquidacao : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Liquidacao", "JurosMulta", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Liquidacao", "Desconto", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Liquidacao", "Desconto", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Liquidacao", "JurosMulta", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
