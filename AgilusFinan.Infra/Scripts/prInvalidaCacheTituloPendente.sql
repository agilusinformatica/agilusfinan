if object_id('prInvalidaCacheTituloPendente') > 0
begin
   drop procedure prInvalidaCacheTituloPendente
   print '<< DROP prInvalidaCacheTituloPendente >>'
end

GO

CREATE procedure prInvalidaCacheTituloPendente(@empresaId int, @data datetime) as
/*----------------------------------------------------------------------------------------------------------------------
NOME: prInvalidaCacheTituloPendente
OBJETIVO: Apagar referencia de titulos pendentes da tabela Cache 
DATA: 18/03/2016
----------------------------------------------------------------------------------------------------------------------*/
begin
	set nocount on 
	
	delete cache
	from cache as c
	where nome = 'titulopendente'
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

if object_id('prInvalidaCacheTituloPendente') > 0
begin
   print '<< CREATE prInvalidaCacheTituloPendente >>'
end
GO


