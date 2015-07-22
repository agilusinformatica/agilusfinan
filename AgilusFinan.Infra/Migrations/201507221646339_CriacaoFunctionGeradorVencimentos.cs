namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoFunctionGeradorVencimentos : DbMigration
    {
        public override void Up()
        {
            SqlFile(@"../../Scripts/fn_gerador_vencimentos.sql", false, null);
        }
        
        public override void Down()
        {
            Sql("drop function fn_gerador_vencimentos");
        }
    }
}
