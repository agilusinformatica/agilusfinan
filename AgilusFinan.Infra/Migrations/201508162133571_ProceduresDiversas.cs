namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProceduresDiversas : DbMigration
    {
        public override void Up()
        {
            SqlFile(@"../../Scripts/pr_cria_titulo_virtual.sql", false, null);
            SqlFile(@"../../Scripts/pr_previsto_realizado.sql", false, null);
            SqlFile(@"../../Scripts/pr_resumo_titulo_categoria.sql", false, null);
            SqlFile(@"../../Scripts/pr_titulos_pendentes.sql", false, null);
        }
        
        public override void Down()
        {
        }
    }
}
