namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoFunctionSomaCategoria : DbMigration
    {
        public override void Up()
        {
            SqlFile(@"../../Scripts/fn_soma_categoria.sql", false, null);
        }
        
        public override void Down()
        {
            Sql("drop function fn_soma_categoria");
        }
    }
}