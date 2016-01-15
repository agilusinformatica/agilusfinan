namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionaAssuntoEmailemModeloBoleto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ModeloBoleto", "AssuntoEmail", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ModeloBoleto", "AssuntoEmail");
        }
    }
}
