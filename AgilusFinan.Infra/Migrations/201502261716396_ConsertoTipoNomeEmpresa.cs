namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConsertoTipoNomeEmpresa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Empresa", "Nome", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Empresa", "Nome", c => c.Int(nullable: false));
        }
    }
}
