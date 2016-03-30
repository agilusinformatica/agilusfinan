namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteCascadeConvite : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Convite", "PerfilId", "dbo.Perfil");
            AddForeignKey("dbo.Convite", "PerfilId", "dbo.Perfil", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Convite", "PerfilId", "dbo.Perfil");
            AddForeignKey("dbo.Convite", "PerfilId", "dbo.Perfil", "Id");
        }
    }
}
