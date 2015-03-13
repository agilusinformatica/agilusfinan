namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoEntidadeTitulo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pagamento", "DataLiquidacao", c => c.DateTime(nullable: false));
            AddColumn("dbo.Pagamento", "ValorLiquidado", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Recebimento", "DataLiquidacao", c => c.DateTime(nullable: false));
            AddColumn("dbo.Recebimento", "ValorLiquidado", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Pagamento", "DataPagamento");
            DropColumn("dbo.Pagamento", "ValorPago");
            DropColumn("dbo.Recebimento", "DataRecebimento");
            DropColumn("dbo.Recebimento", "ValorRecebido");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Recebimento", "ValorRecebido", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Recebimento", "DataRecebimento", c => c.DateTime(nullable: false));
            AddColumn("dbo.Pagamento", "ValorPago", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Pagamento", "DataPagamento", c => c.DateTime(nullable: false));
            DropColumn("dbo.Recebimento", "ValorLiquidado");
            DropColumn("dbo.Recebimento", "DataLiquidacao");
            DropColumn("dbo.Pagamento", "ValorLiquidado");
            DropColumn("dbo.Pagamento", "DataLiquidacao");
        }
    }
}
