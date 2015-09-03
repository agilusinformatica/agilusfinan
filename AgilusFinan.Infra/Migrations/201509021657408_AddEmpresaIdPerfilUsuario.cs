namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmpresaIdPerfilUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Perfil", "EmpresaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Perfil", "EmpresaId");
            CreateIndex("dbo.Usuario", "EmpresaId");
            AddForeignKey("dbo.Perfil", "EmpresaId", "dbo.Empresa", "Id");
            AddForeignKey("dbo.Usuario", "EmpresaId", "dbo.Empresa", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario", "EmpresaId", "dbo.Empresa");
            DropForeignKey("dbo.Perfil", "EmpresaId", "dbo.Empresa");
            DropIndex("dbo.Usuario", new[] { "EmpresaId" });
            DropIndex("dbo.Perfil", new[] { "EmpresaId" });
            DropColumn("dbo.Perfil", "EmpresaId");
        }
    }
}
