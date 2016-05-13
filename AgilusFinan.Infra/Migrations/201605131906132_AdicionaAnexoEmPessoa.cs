namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionaAnexoEmPessoa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pessoa", "Anexo", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pessoa", "Anexo");
        }
    }
}
