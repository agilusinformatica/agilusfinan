namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionaCampoNossoNumeroBoletoGerado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BoletoGerado", "NossoNumero", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BoletoGerado", "NossoNumero");
        }
    }
}
