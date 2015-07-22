namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoProcedureCriaTituloVirtual : DbMigration
    {
        public override void Up()
        {
            SqlFile(@"../../Scripts/pr_cria_titulo_virtual.sql", false, null);
        }
        
        public override void Down()
        {
            Sql("drop procedure pr_cria_titulo_virtual");
        }
    }
}
