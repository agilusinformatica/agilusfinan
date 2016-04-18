namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataNascimentoToNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pessoa", "DataNascimento", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pessoa", "DataNascimento", c => c.DateTime(nullable: false));
        }
    }
}
