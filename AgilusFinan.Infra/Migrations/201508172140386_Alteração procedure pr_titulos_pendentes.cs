namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alteraçãoprocedurepr_titulos_pendentes : DbMigration
    {
        public override void Up()
        {
            SqlFile(@"../../Scripts/pr_titulos_pendentes.sql", false, null);
        }
        
        public override void Down()
        {
        }
    }
}
