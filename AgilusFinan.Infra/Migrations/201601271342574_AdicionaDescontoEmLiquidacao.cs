namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionaDescontoEmLiquidacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Liquidacao", "Desconto", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Liquidacao", "Desconto");
        }
    }
}
