namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmpresaNoCentroCusto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CentroCusto", "EmpresaId", c => c.Int(nullable: false));
            CreateIndex("dbo.CentroCusto", "EmpresaId");
            AddForeignKey("dbo.CentroCusto", "EmpresaId", "dbo.Empresa", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CentroCusto", "EmpresaId", "dbo.Empresa");
            DropIndex("dbo.CentroCusto", new[] { "EmpresaId" });
            DropColumn("dbo.CentroCusto", "EmpresaId");
        }
    }
}
