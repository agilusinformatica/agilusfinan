namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TelefonePessoa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TelefonePessoa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PessoaId = c.Int(nullable: false),
                        Telefone_Ddd = c.String(),
                        Telefone_Numero = c.String(),
                        Telefone_TipoTelefoneId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoa", t => t.PessoaId)
                .Index(t => t.PessoaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TelefonePessoa", "PessoaId", "dbo.Pessoa");
            DropIndex("dbo.TelefonePessoa", new[] { "PessoaId" });
            DropTable("dbo.TelefonePessoa");
        }
    }
}
