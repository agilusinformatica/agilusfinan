namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDataCadastroonTítuloRecorrente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TituloRecorrente", "DataCadastro", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TituloRecorrente", "DataCadastro");
        }
    }
}
