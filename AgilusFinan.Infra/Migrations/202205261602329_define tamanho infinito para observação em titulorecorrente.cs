namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class definetamanhoinfinitoparaobservaçãoemtitulorecorrente : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TituloRecorrente", "Observacao", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TituloRecorrente", "Observacao", c => c.String(maxLength: 1000, unicode: false));
        }
    }
}
