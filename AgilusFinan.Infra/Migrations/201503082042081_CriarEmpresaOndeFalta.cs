namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriarEmpresaOndeFalta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categoria", "EmpresaId", c => c.Int(nullable: false));
            AddColumn("dbo.Transferencia", "EmpresaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Categoria", "EmpresaId");
            CreateIndex("dbo.Transferencia", "EmpresaId");
            AddForeignKey("dbo.Categoria", "EmpresaId", "dbo.Empresa", "Id");
            AddForeignKey("dbo.Transferencia", "EmpresaId", "dbo.Empresa", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transferencia", "EmpresaId", "dbo.Empresa");
            DropForeignKey("dbo.Categoria", "EmpresaId", "dbo.Empresa");
            DropIndex("dbo.Transferencia", new[] { "EmpresaId" });
            DropIndex("dbo.Categoria", new[] { "EmpresaId" });
            DropColumn("dbo.Transferencia", "EmpresaId");
            DropColumn("dbo.Categoria", "EmpresaId");
        }
    }
}
