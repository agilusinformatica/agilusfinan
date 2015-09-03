namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoAcessoFuncao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Acesso",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FuncaoId = c.Int(nullable: false),
                        PerfilId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Funcao", t => t.FuncaoId)
                .ForeignKey("dbo.Perfil", t => t.PerfilId)
                .Index(t => t.FuncaoId)
                .Index(t => t.PerfilId);
            
            CreateTable(
                "dbo.Funcao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 100, unicode: false),
                        Endereco = c.String(maxLength: 100, unicode: false),
                        FuncaoSuperiorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Funcao", t => t.FuncaoSuperiorId)
                .Index(t => t.FuncaoSuperiorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Acesso", "PerfilId", "dbo.Perfil");
            DropForeignKey("dbo.Acesso", "FuncaoId", "dbo.Funcao");
            DropForeignKey("dbo.Funcao", "FuncaoSuperiorId", "dbo.Funcao");
            DropIndex("dbo.Funcao", new[] { "FuncaoSuperiorId" });
            DropIndex("dbo.Acesso", new[] { "PerfilId" });
            DropIndex("dbo.Acesso", new[] { "FuncaoId" });
            DropTable("dbo.Funcao");
            DropTable("dbo.Acesso");
        }
    }
}
