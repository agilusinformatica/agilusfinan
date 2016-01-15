namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoTextoEmailemModeloBoleto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ModeloBoleto", "TextoEmail", c => c.String(maxLength: 8000, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ModeloBoleto", "TextoEmail");
        }
    }
}
