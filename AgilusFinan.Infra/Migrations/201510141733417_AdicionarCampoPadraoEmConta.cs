namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionarCampoPadraoEmConta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conta", "Padrao", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conta", "Padrao");
        }
    }
}
