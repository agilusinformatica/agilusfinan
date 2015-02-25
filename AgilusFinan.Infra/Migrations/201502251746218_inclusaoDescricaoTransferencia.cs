namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inclusaoDescricaoTransferencia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transferencia", "Descricao", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transferencia", "Descricao");
        }
    }
}
