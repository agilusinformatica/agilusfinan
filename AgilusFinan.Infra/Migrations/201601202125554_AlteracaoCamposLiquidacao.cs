namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoCamposLiquidacao : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Liquidacao", "Desconto", c => c.Double());
            AlterColumn("dbo.Liquidacao", "FormaLiquidacao", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Liquidacao", "FormaLiquidacao", c => c.Int(nullable: false));
            //DropColumn("dbo.Liquidacao", "Desconto");
        }
    }
}
