namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConsertoProcedureCriaTituloVirtual : DbMigration
    {
        public override void Up()
        {
            SqlFile(@"../../Scripts/pr_cria_titulo_virtual.sql");
        }
        
        public override void Down()
        {
        }
    }
}
