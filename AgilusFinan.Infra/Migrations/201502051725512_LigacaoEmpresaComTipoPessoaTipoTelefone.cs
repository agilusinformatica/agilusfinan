namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LigacaoEmpresaComTipoPessoaTipoTelefone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TipoPessoa", "EmpresaId", c => c.Int(nullable: false));
            AddColumn("dbo.TipoTelefone", "EmpresaId", c => c.Int(nullable: false));
            CreateIndex("dbo.TipoPessoa", "EmpresaId");
            CreateIndex("dbo.TipoTelefone", "EmpresaId");
            AddForeignKey("dbo.TipoPessoa", "EmpresaId", "dbo.Empresa", "Id");
            AddForeignKey("dbo.TipoTelefone", "EmpresaId", "dbo.Empresa", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TipoTelefone", "EmpresaId", "dbo.Empresa");
            DropForeignKey("dbo.TipoPessoa", "EmpresaId", "dbo.Empresa");
            DropIndex("dbo.TipoTelefone", new[] { "EmpresaId" });
            DropIndex("dbo.TipoPessoa", new[] { "EmpresaId" });
            DropColumn("dbo.TipoTelefone", "EmpresaId");
            DropColumn("dbo.TipoPessoa", "EmpresaId");
        }
    }
}
