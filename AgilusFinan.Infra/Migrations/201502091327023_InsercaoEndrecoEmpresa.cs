namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsercaoEndrecoEmpresa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empresa", "Endereco_Logradouro", c => c.String());
            AddColumn("dbo.Empresa", "Endereco_Numero", c => c.String());
            AddColumn("dbo.Empresa", "Endereco_Complemento", c => c.String());
            AddColumn("dbo.Empresa", "Endereco_Bairro", c => c.String());
            AddColumn("dbo.Empresa", "Endereco_Cidade", c => c.String());
            AddColumn("dbo.Empresa", "Endereco_Uf", c => c.String());
            AddColumn("dbo.Empresa", "Endereco_Cep", c => c.String());
            AddColumn("dbo.Empresa", "Endereco_Pais", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empresa", "Endereco_Pais");
            DropColumn("dbo.Empresa", "Endereco_Cep");
            DropColumn("dbo.Empresa", "Endereco_Uf");
            DropColumn("dbo.Empresa", "Endereco_Cidade");
            DropColumn("dbo.Empresa", "Endereco_Bairro");
            DropColumn("dbo.Empresa", "Endereco_Complemento");
            DropColumn("dbo.Empresa", "Endereco_Numero");
            DropColumn("dbo.Empresa", "Endereco_Logradouro");
        }
    }
}
