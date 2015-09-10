namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pr_extrato : DbMigration
    {
        public override void Up()
        {
            SqlFile(@"../../Scripts/pr_extrato.sql", false, null);
        }
        
        public override void Down()
        {
            Sql("drop procedure pr_extrato");

        }
    }
}
