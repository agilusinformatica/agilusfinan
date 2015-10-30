namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaxLengthNomeBanco30 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Banco", "Nome", c => c.String(maxLength: 30, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Banco", "Nome", c => c.String(maxLength: 100, unicode: false));
        }
    }
}
