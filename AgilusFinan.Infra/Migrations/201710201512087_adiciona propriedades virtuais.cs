namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adicionapropriedadesvirtuais : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.FaturaGerada", "TituloId");
            CreateIndex("dbo.FaturaGerada", "TituloRecorrenteId");
            AddForeignKey("dbo.FaturaGerada", "TituloId", "dbo.Titulo", "Id");
            AddForeignKey("dbo.FaturaGerada", "TituloRecorrenteId", "dbo.TituloRecorrente", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FaturaGerada", "TituloRecorrenteId", "dbo.TituloRecorrente");
            DropForeignKey("dbo.FaturaGerada", "TituloId", "dbo.Titulo");
            DropIndex("dbo.FaturaGerada", new[] { "TituloRecorrenteId" });
            DropIndex("dbo.FaturaGerada", new[] { "TituloId" });
        }
    }
}
