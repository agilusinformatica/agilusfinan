namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCascadeOnPessoaDependents : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TelefonePessoa", "PessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.TipoPessoaPorPessoa", "PessoaId", "dbo.Pessoa");
            AddForeignKey("dbo.TelefonePessoa", "PessoaId", "dbo.Pessoa", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TipoPessoaPorPessoa", "PessoaId", "dbo.Pessoa", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TipoPessoaPorPessoa", "PessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.TelefonePessoa", "PessoaId", "dbo.Pessoa");
            AddForeignKey("dbo.TipoPessoaPorPessoa", "PessoaId", "dbo.Pessoa", "Id");
            AddForeignKey("dbo.TelefonePessoa", "PessoaId", "dbo.Pessoa", "Id");
        }
    }
}
