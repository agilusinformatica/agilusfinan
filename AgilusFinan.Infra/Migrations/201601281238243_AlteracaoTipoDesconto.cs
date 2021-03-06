namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoTipoDesconto : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Liquidacao", "Desconto", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Liquidacao", "Desconto", c => c.Double());
        }
    }
}
