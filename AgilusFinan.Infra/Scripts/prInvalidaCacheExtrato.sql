if object_id('prInvalidaCacheExtrato') > 0
begin
   drop procedure prInvalidaCacheExtrato
   print '<< DROP prInvalidaCacheExtrato >>'
end

GO

create procedure prInvalidaCacheExtrato(@contaId int, @data datetime) as
/*----------------------------------------------------------------------------------------------------------------------
NOME: prInvalidaCacheExtrato
OBJETIVO: Apagar referência Cache extrato
DATA: 18/03/2016
----------------------------------------------------------------------------------------------------------------------*/
begin
	set nocount on 
	
	delete cache
	from cache as c
	where nome = 'extrato'
	and exists( select 1
				  from parametrocache
				  where CacheId = c.Id
				  and nome = 'ContaId'
				  and valor = @contaId )
	and exists( select 1
				  from parametrocache
				  where CacheId = c.Id
				  and nome = 'data_final'
				  and @data <= convert(datetime,valor,111) )

end

GO

if object_id('prInvalidaCacheExtrato') > 0
begin
   print '<< CREATE prInvalidaCacheExtrato >>'
end
GO


