namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AceitaNullDataFinalTituloRecorrente : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TituloRecorrente", "DataFinal", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TituloRecorrente", "DataFinal", c => c.DateTime(nullable: false));
        }
    }
}
