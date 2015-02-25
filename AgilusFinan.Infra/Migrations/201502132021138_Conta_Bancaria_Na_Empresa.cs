namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Conta_Bancaria_Na_Empresa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empresa", "ContaBancaria_BancoId", c => c.Int(nullable: false));
            AddColumn("dbo.Empresa", "ContaBancaria_Agencia", c => c.String());
            AddColumn("dbo.Empresa", "ContaBancaria_Numero", c => c.String());
            AddColumn("dbo.Empresa", "ContaBancaria_Poupanca", c => c.Boolean(nullable: false));
            AddColumn("dbo.Empresa", "ContaBancaria_NomeTitular", c => c.String());
            AddColumn("dbo.Empresa", "ContaBancaria_CpfTitular", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empresa", "ContaBancaria_CpfTitular");
            DropColumn("dbo.Empresa", "ContaBancaria_NomeTitular");
            DropColumn("dbo.Empresa", "ContaBancaria_Poupanca");
            DropColumn("dbo.Empresa", "ContaBancaria_Numero");
            DropColumn("dbo.Empresa", "ContaBancaria_Agencia");
            DropColumn("dbo.Empresa", "ContaBancaria_BancoId");
        }
    }
}
