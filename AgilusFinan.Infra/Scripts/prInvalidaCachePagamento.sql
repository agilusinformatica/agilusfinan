if object_id('prInvalidaCachePagamento') > 0
begin
   drop procedure prInvalidaCachePagamento
   print '<< DROP prInvalidaCachePagamento >>'
end

GO

create procedure prInvalidaCachePagamento(@empresaId int, @data datetime) as
/*----------------------------------------------------------------------------------------------------------------------
NOME: prInvalidaCachePagamento
OBJETIVO: Apagar referencia de pagamento da tabela Cache 
DATA: 18/03/2016
----------------------------------------------------------------------------------------------------------------------*/
begin
	set nocount on 
	
	delete cache
	from cache as c
	where nome = 'pagamento'
	and exists( select 1
				  from parametrocache
				  where CacheId = c.Id
				  and nome = 'empresaId'
				  and valor = @empresaId )
	and exists( select 1
				  from parametrocache
				  where CacheId = c.Id
				  and nome = 'dataInicial'
				  and @data >= convert(datetime,valor,103) )
	and exists( select 1
				  from parametrocache
				  where CacheId = c.Id
				  and nome = 'dataFinal'
				  and @data <= convert(datetime,valor,103) )

end

GO

if object_id('prInvalidaCachePagamento') > 0
begin
   print '<< CREATE prInvalidaCachePagamento >>'
end
GO


