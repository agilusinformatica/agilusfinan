namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionaEmailemPessoa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pessoa", "EmailContato", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.Pessoa", "EmailFinanceiro", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pessoa", "EmailFinanceiro");
            DropColumn("dbo.Pessoa", "EmailContato");
        }
    }
}
