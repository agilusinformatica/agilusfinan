if object_id('prInvalidaCacheFluxoCaixa') > 0
begin
   drop procedure prInvalidaCacheFluxoCaixa
   print '<< DROP prInvalidaCacheFluxoCaixa >>'
end

GO

create procedure prInvalidaCacheFluxoCaixa(@empresaId int, @data datetime) as
/*----------------------------------------------------------------------------------------------------------------------
NOME: prInvalidaCacheFluxoCaixa
OBJETIVO: Apagar referência Cache Fluxo Caixa
DATA: 18/03/2016
----------------------------------------------------------------------------------------------------------------------*/
begin
	set nocount on 

	delete cache
	from cache as c
	where nome = 'fluxocaixa'
	and exists( select 1
				  from parametrocache
				  where CacheId = c.Id
				  and nome = 'empresaId'
				  and valor = @empresaId )
	and exists( select 1
				  from parametrocache
				  where CacheId = c.Id
				  and nome = 'dataFinal'
				  and @data <= convert(datetime,valor,103))

end

GO

if object_id('prInvalidaCacheFluxoCaixa') > 0
begin
   print '<< CREATE prInvalidaCacheFluxoCaixa >>'
end
GO


