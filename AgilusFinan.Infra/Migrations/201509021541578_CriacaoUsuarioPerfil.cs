namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoUsuarioPerfil : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 100, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        PerfilId = c.Int(nullable: false),
                        Senha = c.String(maxLength: 100, unicode: false),
                        Endereco_Logradouro = c.String(maxLength: 100, unicode: false),
                        Endereco_Numero = c.String(maxLength: 100, unicode: false),
                        Endereco_Complemento = c.String(maxLength: 100, unicode: false),
                        Endereco_Bairro = c.String(maxLength: 100, unicode: false),
                        Endereco_Cidade = c.String(maxLength: 100, unicode: false),
                        Endereco_Uf = c.String(maxLength: 100, unicode: false),
                        Endereco_Cep = c.String(maxLength: 100, unicode: false),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Perfil", t => t.PerfilId)
                .Index(t => t.PerfilId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario", "PerfilId", "dbo.Perfil");
            DropIndex("dbo.Usuario", new[] { "PerfilId" });
            DropTable("dbo.Usuario");
            DropTable("dbo.Perfil");
        }
    }
}
