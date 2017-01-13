namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteraCampodeTextoEmailLiquidaçãonatabelaparametrosparaumtamanhoMAX : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Parametro", "TextoEmailLiquidacao", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Parametro", "TextoEmailLiquidacao", c => c.String(maxLength: 100, unicode: false));
        }
    }
}
