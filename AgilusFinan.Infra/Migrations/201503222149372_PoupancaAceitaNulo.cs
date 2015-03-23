namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PoupancaAceitaNulo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pessoa", "ContaBancaria_Poupanca", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pessoa", "ContaBancaria_Poupanca", c => c.Boolean());
        }
    }
}
