namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MudancaTamanhoCampoObservacaoTituloPara500 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Titulo", "Observacao", c => c.String(maxLength: 500, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Titulo", "Observacao", c => c.String(maxLength: 50, unicode: false));
        }
    }
}
