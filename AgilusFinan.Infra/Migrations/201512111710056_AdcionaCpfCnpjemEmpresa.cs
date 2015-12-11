namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdcionaCpfCnpjemEmpresa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empresa", "CpfCnpj", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empresa", "CpfCnpj");
        }
    }
}
