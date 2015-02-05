namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoTipoPessoa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TipoPessoa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TipoPessoaPorPessoa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoPessoaId = c.Int(nullable: false),
                        PessoaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoa", t => t.PessoaId)
                .ForeignKey("dbo.TipoPessoa", t => t.TipoPessoaId)
                .Index(t => t.TipoPessoaId)
                .Index(t => t.PessoaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TipoPessoaPorPessoa", "TipoPessoaId", "dbo.TipoPessoa");
            DropForeignKey("dbo.TipoPessoaPorPessoa", "PessoaId", "dbo.Pessoa");
            DropIndex("dbo.TipoPessoaPorPessoa", new[] { "PessoaId" });
            DropIndex("dbo.TipoPessoaPorPessoa", new[] { "TipoPessoaId" });
            DropTable("dbo.TipoPessoaPorPessoa");
            DropTable("dbo.TipoPessoa");
        }
    }
}
