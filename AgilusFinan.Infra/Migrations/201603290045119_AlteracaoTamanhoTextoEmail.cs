namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoTamanhoTextoEmail : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ModeloBoleto", "TextoEmail", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ModeloBoleto", "TextoEmail", c => c.String(maxLength: 8000, unicode: false));
        }
    }
}
