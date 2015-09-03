namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCampoAtivoEmEmpresaEUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empresa", "Ativo", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("dbo.Usuario", "Ativo", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "Ativo");
            DropColumn("dbo.Empresa", "Ativo");
        }
    }
}
