namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteCascadeOnPerfil : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Acesso", "PerfilId", "dbo.Perfil");
            AddForeignKey("dbo.Acesso", "PerfilId", "dbo.Perfil", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Acesso", "PerfilId", "dbo.Perfil");
            AddForeignKey("dbo.Acesso", "PerfilId", "dbo.Perfil", "Id");
        }
    }
}
