if object_id('prInvalidaCacheRecebimento') > 0
begin
   drop procedure prInvalidaCacheRecebimento
   print '<< DROP prInvalidaCacheRecebimento >>'
end

GO

create procedure prInvalidaCacheRecebimento(@empresaId int, @data datetime) as
/*----------------------------------------------------------------------------------------------------------------------
NOME: prInvalidaCacheRecebimento
OBJETIVO: Apagar referencia de recebimento da tabela Cache 
DATA: 18/03/2016
----------------------------------------------------------------------------------------------------------------------*/
begin
	set nocount on 
	
	delete cache
	from cache as c
	where nome = 'recebimento'
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

if object_id('prInvalidaCacheRecebimento') > 0
begin
   print '<< CREATE prInvalidaCacheRecebimento >>'
end
GO


