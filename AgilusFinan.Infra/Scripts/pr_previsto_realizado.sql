if object_id('pr_previsto_realizado') > 0
begin
   drop procedure pr_previsto_realizado
   print '<< DROP pr_previsto_realizado >>'
end

GO

create procedure pr_previsto_realizado(@id_empresa int, @data_inicial smalldatetime, @data_final smalldatetime)
/*----------------------------------------------------------------------------------------------------------------------
NOME: pr_cria_titulo_virtual
OBJETIVO: Consulta de valores dos títulos por categoria
DATA: 07/08/2015
----------------------------------------------------------------------------------------------------------------------
exec pr_previsto_realizado 2,'20150701','20150831'
----------------------------------------------------------------------------------------------------------------------*/
as 
Begin
	create table #temp (TituloRecorrenteId int, nome varchar(100), dataVencimento smalldatetime, valor money, CategoriaId int,
	                             PessoaId int, CentroCustoId int, TituloId int)

	insert into #temp
	exec pr_cria_titulo_virtual @id_empresa, @data_inicial, @data_final
	
	declare @recebimento_previsto money,
			@recebimento_realizado money,
			@pagamento_previsto money,
			@pagamento_realizado money

	select @recebimento_previsto = SUM(case when c.Direcao = 0 then Valor else 0 end), 
	@pagamento_previsto = SUM(case when c.Direcao = 1 then Valor else 0 end)
	from #temp t
	join Categoria c on t.CategoriaId = c.Id

	select @recebimento_realizado = SUM(case when c.Direcao = 0 then l.Valor else 0 end), 
	@pagamento_realizado = SUM(case when c.Direcao = 1 then l.Valor else 0 end)
	from #temp t
	join Categoria c on t.CategoriaId = c.Id
	join Liquidacao l on l.TituloId = t.TituloId

	select 'Pagamento' Tipo, isnull(@pagamento_previsto, 0) Previsto, isnull(@pagamento_realizado, 0) Realizado
	union
	select 'Recebimento', isnull(@recebimento_previsto, 0), isnull(@recebimento_realizado, 0)

end
GO

if object_id('pr_previsto_realizado') > 0
begin
   print '<< CREATE pr_previsto_realizado >>'
end
GO

