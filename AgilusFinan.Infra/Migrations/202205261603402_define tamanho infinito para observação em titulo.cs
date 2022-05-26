namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class definetamanhoinfinitoparaobservaçãoemtitulo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Titulo", "Observacao", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Titulo", "Observacao", c => c.String(maxLength: 8000, unicode: false));
        }
    }
}
