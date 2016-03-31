if object_id('pr_soma_categoria') > 0
begin
   drop procedure pr_soma_categoria
   print '<< DROP pr_soma_categoria >>'
end
GO



create procedure pr_soma_categoria(@id int, @data_inicial smalldatetime, @data_final smalldatetime, @soma_prevista decimal(18,2) output, @soma_realizada decimal(18,2) output)
as
/*----------------------------------------------------------------------------------------------------------------------
NOME: fn_soma_categoria
OBJETIVO: Somar valores de títulos criados por categorias e categorias dependentes
DATA: 07/08/2015
EXEMPLO: declare @soma decimal(18,2) exec pr_soma_categoria 2, '2016-03-01', '2016-03-31', @soma output select @soma
----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------*/
begin
	declare @soma_filha_prevista decimal(18,2) = 0
	declare @soma_filha_realizada decimal(18,2) = 0

	declare @EmpresaId int
	
	select @EmpresaId = EmpresaId 
	from categoria
	where Id = @Id

	create table #titulo (TituloRecorrenteId int, nome varchar(100), dataVencimento smalldatetime, valor money, CategoriaId int, ContaId int, PessoaId int, CentroCustoId int, TituloId int)

	insert into #titulo
	exec pr_cria_titulo_virtual @EmpresaId, @data_inicial, @data_final

	select @soma_prevista = sum(valor)
	from #titulo
	where CategoriaId = @id
	and DataVencimento >= @data_inicial
	and DataVencimento < @data_final + 1

	select @soma_realizada = SUM(l.Valor+isnull(l.JurosMulta,0.0) - isnull(l.Desconto,0.0))
	from #titulo t
	join Liquidacao l on  l.tituloId = t.tituloId
	where categoriaId = @id 
	and t.DataVencimento >= @data_inicial
	and t.DataVencimento < @data_final + 1

	declare @idFilha int
	declare curSoma cursor local for
	select id
	from Categoria
	where CategoriaPaiId = @id

	open curSoma
	fetch curSoma into @idFilha

	While @@FETCH_STATUS = 0
	begin
		exec pr_soma_categoria @idFilha, @data_inicial, @data_final, @soma_filha_prevista output, @soma_filha_realizada output
		set @soma_prevista = isnull(@soma_prevista,0) + isnull(@soma_filha_prevista,0)
		set @soma_realizada = isnull(@soma_realizada,0) + isnull(@soma_filha_realizada,0)
		fetch curSoma into @idFilha
	end

	close curSoma
	deallocate curSoma

end
GO

if object_id('pr_soma_categoria') > 0
begin
   print '<< CREATE pr_soma_categoria >>'
end
GO

