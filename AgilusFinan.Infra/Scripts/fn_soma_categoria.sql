if object_id('fn_soma_categoria') > 0
begin
   drop function fn_soma_categoria
   print '<< DROP fn_soma_categoria >>'
end
GO

create function dbo.fn_soma_categoria(@id int, @data_inicial smalldatetime, @data_final smalldatetime)
returns decimal(18,2)
/*----------------------------------------------------------------------------------------------------------------------
NOME: fn_soma_categoria
OBJETIVO: Somar valores de títulos criados por categorias e categorias dependentes
DATA: 07/08/2015
EXEMPLO: select id, Nome, CategoriaPaiId, Cor, dbo.fn_soma_categoria(id, '20150101','20151231') from Categoria
----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------*/
begin
	declare @soma decimal(18,2) = 0

	select @soma = sum(valor)
	from titulo
	where CategoriaId = @id
	and DataVencimento >= @data_inicial
	and DataVencimento < @data_final + 1

	declare @idFilha int
	declare curSoma cursor for
	select id
	from Categoria
	where CategoriaPaiId = @id

	open curSoma
	fetch curSoma into @idFilha

	While @@FETCH_STATUS = 0
	begin
		set @soma = isnull(@soma,0) + isnull(dbo.fn_soma_categoria(@idFilha, @data_inicial, @data_final),0)
		fetch curSoma into @idFilha
	end

	close curSoma
	deallocate curSoma

	return isnull(@soma,0)
end
GO

if object_id('fn_soma_categoria') > 0
begin
   print '<< CREATE fn_soma_categoria >>'
end
GO