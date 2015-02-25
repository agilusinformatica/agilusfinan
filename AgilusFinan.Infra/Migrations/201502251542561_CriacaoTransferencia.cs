namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoTransferencia : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transferencia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContaOrigemId = c.Int(nullable: false),
                        ContaDestinoId = c.Int(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conta", t => t.ContaDestinoId)
                .ForeignKey("dbo.Conta", t => t.ContaOrigemId)
                .Index(t => t.ContaOrigemId)
                .Index(t => t.ContaDestinoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transferencia", "ContaOrigemId", "dbo.Conta");
            DropForeignKey("dbo.Transferencia", "ContaDestinoId", "dbo.Conta");
            DropIndex("dbo.Transferencia", new[] { "ContaDestinoId" });
            DropIndex("dbo.Transferencia", new[] { "ContaOrigemId" });
            DropTable("dbo.Transferencia");
        }
    }
}
