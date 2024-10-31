namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adicionaurlfaturacheckoutemfaturagerada : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FaturaGerada", "UrlFaturaCheckout", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FaturaGerada", "UrlFaturaCheckout");
        }
    }
}
