namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUniqueIndexOnTitulo : DbMigration
    {
        public override void Up()
        {
            Sql(@"if not exists(select 1 from sys.indexes where name = 'i_dataVencimento' and object_name(object_id) = 'Titulo')
                    create unique index i_dataVencimento on titulo(DataVencimento, TituloRecorrenteId) where TituloRecorrenteId is not null");
        }
        
        public override void Down()
        {
            Sql(@"if exists(select 1 from sys.indexes where name = 'i_dataVencimento' and object_name(object_id) = 'Titulo')
                    drop index i_dataVencimento on Titulo");
        }
    }
}
