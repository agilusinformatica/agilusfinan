namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atualizaProcedures : DbMigration
    {
        public override void Up()
        {
            SqlFile(@"../../Scripts/pr_cria_titulo_virtual.sql", false, null);
        }
        
        public override void Down()
        {
        }
    }
}
