namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionaPessoaIndicacaoemPessoa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pessoa", "PessoaIndicacaoId", c => c.Int());
            CreateIndex("dbo.Pessoa", "PessoaIndicacaoId");
            AddForeignKey("dbo.Pessoa", "PessoaIndicacaoId", "dbo.Pessoa", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pessoa", "PessoaIndicacaoId", "dbo.Pessoa");
            DropIndex("dbo.Pessoa", new[] { "PessoaIndicacaoId" });
            DropColumn("dbo.Pessoa", "PessoaIndicacaoId");
        }
    }
}
