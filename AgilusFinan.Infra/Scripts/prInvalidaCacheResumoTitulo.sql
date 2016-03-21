if object_id('prInvalidaCacheResumoTitulo') > 0
begin
   drop procedure prInvalidaCacheResumoTitulo
   print '<< DROP prInvalidaCacheResumoTitulo >>'
end

GO

create procedure prInvalidaCacheResumoTitulo(@empresaId int, @dataInicial datetime, @dataFinal) as
/*----------------------------------------------------------------------------------------------------------------------
NOME: prInvalidaCacheResumoTitulo
OBJETIVO: Apagar referencia de saldo da tabela Cache 
DATA: 18/03/2016
----------------------------------------------------------------------------------------------------------------------*/
begin
	set nocount on 
	
	delete cache
	from cache as c
	where nome = 'resumotitulo'
	and exists( select 1
				  from parametrocache
				  where CacheId = c.Id
				  and nome = 'empresaId'
				  and valor = @empresaId )
	and exists( select 1
				  from parametrocache
				  where CacheId = c.Id
				  and nome = 'dataInicial'
				  and @dataInicial >= convert(datetime,valor,103) )
	and exists( select 1
				  from parametrocache
				  where CacheId = c.Id
				  and nome = 'dataFinal'
				  and @dataFinal <= convert(datetime,valor,103) )

end

GO

if object_id('prInvalidaCacheResumoTitulo') > 0
begin
   print '<< CREATE prInvalidaCacheResumoTitulo >>'
end
GO


