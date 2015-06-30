namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoverEmpresadeLiquidação : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Liquidacao", "EmpresaId", "dbo.Empresa");
            DropIndex("dbo.Liquidacao", new[] { "EmpresaId" });
            DropColumn("dbo.Liquidacao", "EmpresaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Liquidacao", "EmpresaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Liquidacao", "EmpresaId");
            AddForeignKey("dbo.Liquidacao", "EmpresaId", "dbo.Empresa", "Id");
        }
    }
}
