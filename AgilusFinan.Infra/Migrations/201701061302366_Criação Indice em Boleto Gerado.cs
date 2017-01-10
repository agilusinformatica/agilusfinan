namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriaçãoIndiceemBoletoGerado : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.BoletoGerado", new [] {"ModeloBoletoId", "NossoNumero"}, true, "uIx_ModeloBoletoIdNossoNumero");
        }
        
        public override void Down()
        {
            DropIndex("dbo.BoletoGerado", "uIx_ModeloBoletoIdNossoNumero");
        }
    }
}
