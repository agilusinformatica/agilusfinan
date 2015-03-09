namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InclusaoEmpresaNaClasseConta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conta", "EmpresaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Conta", "EmpresaId");
            AddForeignKey("dbo.Conta", "EmpresaId", "dbo.Empresa", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Conta", "EmpresaId", "dbo.Empresa");
            DropIndex("dbo.Conta", new[] { "EmpresaId" });
            DropColumn("dbo.Conta", "EmpresaId");
        }
    }
}
