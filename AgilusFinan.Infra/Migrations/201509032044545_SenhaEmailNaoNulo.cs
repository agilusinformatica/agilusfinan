namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SenhaEmailNaoNulo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuario", "Email", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Usuario", "Senha", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuario", "Senha", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Usuario", "Email", c => c.String(maxLength: 100, unicode: false));
        }
    }
}
