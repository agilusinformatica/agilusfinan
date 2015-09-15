namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Entidade_Esquecimento_Senha : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EsquecimentoSenha",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UsuarioId = c.Int(nullable: false),
                        Expirado = c.Boolean(nullable: false),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresa", t => t.EmpresaId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId)
                .Index(t => t.EmpresaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EsquecimentoSenha", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.EsquecimentoSenha", "EmpresaId", "dbo.Empresa");
            DropIndex("dbo.EsquecimentoSenha", new[] { "EmpresaId" });
            DropIndex("dbo.EsquecimentoSenha", new[] { "UsuarioId" });
            DropTable("dbo.EsquecimentoSenha");
        }
    }
}
