namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inclusao_pais : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pessoa", "Endereco_Pais", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pessoa", "Endereco_Pais");
        }
    }
}
