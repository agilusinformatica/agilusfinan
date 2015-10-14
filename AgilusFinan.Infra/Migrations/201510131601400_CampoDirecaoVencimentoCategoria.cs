namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoDirecaoVencimentoCategoria : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categoria", "DirecaoVencimentoDiaNaoUtil", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categoria", "DirecaoVencimentoDiaNaoUtil");
        }
    }
}
