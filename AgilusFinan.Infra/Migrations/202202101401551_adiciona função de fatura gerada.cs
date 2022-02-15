namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adicionafunçãodefaturagerada : DbMigration
    {
        public override void Up()
        {
            Sql(
                @"
                declare @existe int

                select @existe = Id
			                    from Funcao
			                    where Endereco like '%FaturaGerada/Index%'

                if (select @existe) is null
                begin
	                insert into Funcao
	                values
	                ('Faturas', 'FaturaGerada/Index', null)
                end 
                ");
        }
        
        public override void Down()
        {
        }
    }
}
