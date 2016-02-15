namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionaDataVencimentoEmBoletoGerado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BoletoGerado", "DataVencimento", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BoletoGerado", "DataVencimento");
        }
    }
}
