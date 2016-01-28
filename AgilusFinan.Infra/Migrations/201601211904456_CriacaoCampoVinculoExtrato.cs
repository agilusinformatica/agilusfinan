namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoCampoVinculoExtrato : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Liquidacao", "ConciliacaoExtratoId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Liquidacao", "ConciliacaoExtratoId");
        }
    }
}
