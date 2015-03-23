namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemocaoPaisClasseEndereco : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Empresa", "Endereco_Pais");
            DropColumn("dbo.Pessoa", "Endereco_Pais");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pessoa", "Endereco_Pais", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.Empresa", "Endereco_Pais", c => c.String(maxLength: 100, unicode: false));
        }
    }
}
