namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoProcedureConsultaResumoTituloCategoria : DbMigration
    {
        public override void Up()
        {
            SqlFile(@"../../Scripts/pr_resumo_titulo_categoria.sql", false, null);
        }

        public override void Down()
        {
            Sql("drop procedure pr_resumo_titulo_categoria");
        }
    }
}
