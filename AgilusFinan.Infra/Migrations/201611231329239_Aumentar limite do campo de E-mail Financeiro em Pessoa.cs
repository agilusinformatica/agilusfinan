namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AumentarlimitedocampodeEmailFinanceiroemPessoa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pessoa", "EmailFinanceiro", c => c.String(maxLength: 500, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pessoa", "EmailFinanceiro", c => c.String(maxLength: 100, unicode: false));
        }
    }
}
