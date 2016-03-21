if object_id('prInvalidaCacheSaldo') > 0
begin
   drop procedure prInvalidaCacheSaldo
   print '<< DROP prInvalidaCacheSaldo >>'
end

GO

create procedure prInvalidaCacheSaldo(@empresaId int, @data datetime) as
/*----------------------------------------------------------------------------------------------------------------------
NOME: prInvalidaCacheSaldo
OBJETIVO: Apagar referencia de saldo da tabela Cache 
DATA: 18/03/2016
----------------------------------------------------------------------------------------------------------------------*/
begin
	set nocount on 
	
	delete cache
	from cache as c
	where nome = 'saldo'
	and exists( select 1
				  from parametrocache
				  where CacheId = c.Id
				  and nome = 'empresaId'
				  and valor = @empresaId )
	and exists( select 1
				  from parametrocache
				  where CacheId = c.Id
				  and nome = 'data'
				  and @data <= convert(datetime,valor,103))

end

GO

if object_id('prInvalidaCacheSaldo') > 0
begin
   print '<< CREATE prInvalidaCacheSaldo >>'
end
GO


