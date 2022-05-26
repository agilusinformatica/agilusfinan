namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class definetamanhomaximoparaobservaçãoemtitulorecorrente : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Titulo", "Observacao", c => c.String(maxLength: 8000, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Titulo", "Observacao", c => c.String(maxLength: 1000, unicode: false));
        }
    }
}
