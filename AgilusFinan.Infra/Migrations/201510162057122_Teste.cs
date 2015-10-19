namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Teste : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Conta", "Padrao");
            AddColumn("dbo.Conta", "Padrao", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conta", "Padrao");
        }
    }
}
