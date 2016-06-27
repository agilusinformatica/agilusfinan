namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionaObservacaoPessoaeDataFinalTituloRecorrente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pessoa", "Observacao", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.TituloRecorrente", "DataFinal", c => c.DateTime(nullable: false));
            AddColumn("dbo.TituloRecorrente", "Observacao", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TituloRecorrente", "Observacao");
            DropColumn("dbo.TituloRecorrente", "DataFinal");
            DropColumn("dbo.Pessoa", "Observacao");
        }
    }
}
