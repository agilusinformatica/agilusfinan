namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoProcedureListarAcessos : DbMigration
    {
        public override void Up()
        {
            SqlFile(@"../../Scripts/pr_listar_acessos.sql", false, null);
        }

        public override void Down()
        {
        }
    }
}
