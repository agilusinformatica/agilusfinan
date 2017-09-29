namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionaCampoTokenIUGUemParametro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parametro", "TokenIUGU", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Parametro", "TokenIUGU");
        }
    }
}
