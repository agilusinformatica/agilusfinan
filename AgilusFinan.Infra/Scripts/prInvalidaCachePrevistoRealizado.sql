if object_id('prInvalidaCachePrevistoRealizado') > 0
begin
   drop procedure prInvalidaCachePrevistoRealizado
   print '<< DROP prInvalidaCachePrevistoRealizado >>'
end

GO

create procedure prInvalidaCachePrevistoRealizado(@empresaId int, @data datetime) as
/*----------------------------------------------------------------------------------------------------------------------
NOME: prInvalidaCachePrevistoRealizado
OBJETIVO: Apagar referencia de Previsto x Realizado da tabela Cache 
DATA: 21/03/2016
----------------------------------------------------------------------------------------------------------------------*/
begin
	set nocount on 
	
	delete cache
	from cache as c
	where nome = 'previstorealizado'
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

if object_id('prInvalidaCachePrevistoRealizado') > 0
begin
   print '<< CREATE prInvalidaCachePrevistoRealizado >>'
end
GO




