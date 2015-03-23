namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionaDataNascimentoPessoa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pessoa", "DataNascimento", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pessoa", "DataNascimento");
        }
    }
}
