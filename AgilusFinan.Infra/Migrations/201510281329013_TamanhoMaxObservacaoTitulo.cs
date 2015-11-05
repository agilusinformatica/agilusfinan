namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TamanhoMaxObservacaoTitulo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Titulo", "Observacao", c => c.String(maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Titulo", "Observacao", c => c.String(maxLength: 100, unicode: false));
        }
    }
}
