if object_id('tr_InvalidaCacheFaturaGerada') > 0
begin
   drop trigger tr_InvalidaCacheFaturaGerada
   print '<< DROP tr_InvalidaCacheFaturaGerada >>'
end

GO


CREATE trigger tr_InvalidaCacheFaturaGerada on FaturaGerada for INSERT, UPDATE, DELETE as
/*----------------------------------------------------------------------------------------------------------------------
NOME: tr_InvalidaCacheFaturaGerada
OBJETIVO: Invalidar cache de fatura gerada
DATA: 26/10/2017
OBSERVAÇÃO: Para esta trigger funcionar é necessário criação da proc: prInvalidaCacheFaturaGerada
O arquivo de criação está localizado na pasta Scripts (Projeto Infra).
----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------*/
begin
	declare @EmpresaId int,  @data datetime
	declare cur cursor for
	select distinct i.EmpresaId, i.DataVencimento
	from inserted as i

	open cur
	fetch cur into @empresaId, @data
	while @@FETCH_STATUS = 0
	begin
		delete cache 
		from cache c
		where Nome in ('fatura')
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

		fetch cur into @empresaId, @data
	end
	close cur
	deallocate cur
	---------------------------------------
	declare cur cursor for
	select distinct d.EmpresaId, d.DataVencimento
	from deleted as d

	open cur
	fetch cur into @empresaId, @data
	while @@FETCH_STATUS = 0
	begin
		delete cache 
		from cache c
		where Nome in ('fatura')
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

		fetch cur into @empresaId, @data
	end
	close cur
	deallocate cur
end
GO

if object_id('tr_InvalidaCacheFaturaGerada') > 0
begin
   print '<< CREATE tr_InvalidaCacheFaturaGerada >>'
end
GO

	
