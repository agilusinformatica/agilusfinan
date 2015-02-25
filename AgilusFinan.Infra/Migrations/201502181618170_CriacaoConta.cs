namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoConta : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        SaldoInicial = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataSaldoInicial = c.DateTime(nullable: false),
                        BancoBoletoId = c.Int(nullable: false),
                        Agencia = c.String(),
                        ContaCorrente = c.String(),
                        Carteira = c.String(),
                        Convenio = c.String(),
                        CodigoCedente = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Banco", t => t.BancoBoletoId)
                .Index(t => t.BancoBoletoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Conta", "BancoBoletoId", "dbo.Banco");
            DropIndex("dbo.Conta", new[] { "BancoBoletoId" });
            DropTable("dbo.Conta");
        }
    }
}
