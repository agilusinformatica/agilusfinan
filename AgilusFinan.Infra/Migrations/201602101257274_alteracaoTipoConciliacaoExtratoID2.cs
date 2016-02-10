namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alteracaoTipoConciliacaoExtratoID2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Liquidacao", "ConciliacaoExtratoId", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Liquidacao", "ConciliacaoExtratoId", c => c.Int());
        }
    }
}
