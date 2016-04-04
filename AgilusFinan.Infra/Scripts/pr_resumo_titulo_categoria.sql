if object_id('pr_resumo_titulo_categoria') > 0
begin
   drop procedure pr_resumo_titulo_categoria
   print '<< DROP pr_resumo_titulo_categoria >>'
end

GO

create procedure pr_resumo_titulo_categoria(@id_empresa int, @data_inicial smalldatetime, @data_final smalldatetime)
/*----------------------------------------------------------------------------------------------------------------------
NOME: pr_resumo_titulo_categoria
OBJETIVO: Consulta de valores dos títulos por categoria
DATA: 07/08/2015
EXEMPLO: exec pr_resumo_titulo_categoria 1, '2016-03-01', '2016-03-31'
----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------*/
as 
Begin
	create table #temp (Id int not null primary key, Nome varchar(100), CategoriaPaiId int, Cor int, ValorPrevisto decimal(18,2), ValorRealizado decimal(18,2) )
	
	insert into #temp 
	select Id as CategoriaId, Nome, CategoriaPaiId, Cor, 0.0, 0.0
	from Categoria 
	where EmpresaId = @id_empresa
	and 1 = 2
	order by Direcao, CategoriaPaiId

	declare @valor_previsto decimal(18,2)
	declare @valor_realizado decimal(18,2)
	declare @Id int
	declare cur cursor local for
	select Id
	from #temp

	open cur
	fetch cur into @Id

	While @@FETCH_STATUS = 0
	begin
		exec pr_soma_categoria @Id, @data_inicial, @data_final, @valor_previsto output, @valor_realizado output
		update #temp
		set ValorPrevisto = isnull(@valor_previsto, 0),
		ValorRealizado = isnull(@valor_realizado, 0)
		where id = @Id

		fetch cur into @id
	end

	close cur
	deallocate cur

	select Id as CategoriaId, Nome, CategoriaPaiId, Cor, ValorPrevisto, ValorRealizado
	from #temp
end
GO

if object_id('pr_resumo_titulo_categoria') > 0
begin
   print '<< CREATE pr_resumo_titulo_categoria >>'
end
GO

