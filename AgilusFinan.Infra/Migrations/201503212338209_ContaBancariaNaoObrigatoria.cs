namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContaBancariaNaoObrigatoria : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pessoa", "ContaBancaria_BancoId", c => c.Int());
            AlterColumn("dbo.Pessoa", "ContaBancaria_Poupanca", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pessoa", "ContaBancaria_Poupanca", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Pessoa", "ContaBancaria_BancoId", c => c.Int(nullable: false));
        }
    }
}
