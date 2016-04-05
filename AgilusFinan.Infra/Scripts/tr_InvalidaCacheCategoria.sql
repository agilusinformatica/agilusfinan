if object_id('tr_InvalidaCacheCategoria') > 0
begin
   drop trigger tr_InvalidaCacheCategoria
   print '<< DROP tr_InvalidaCacheCategoria >>'
end

GO

CREATE trigger tr_InvalidaCacheCategoria on Categoria for INSERT, UPDATE, DELETE as
/*----------------------------------------------------------------------------------------------------------------------
NOME: tr_InvalidaCacheCategoria
OBJETIVO: Invalidar cache de categoria
DATA: 18/03/2016
OBSERVAÇÃO: Para esta trigger funcionar é necessário criação da proc: prInvalidaCacheTituloPendente
O arquivo de criação está localizado na pasta Scripts (Projeto Infra).
----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------*/
begin
	declare @EmpresaId int
	declare cur cursor for
	select distinct i.EmpresaId
	from inserted as i

	open cur
	fetch cur into @empresaId
	while @@FETCH_STATUS = 0
	begin
		delete cache 
		from cache c
		where Nome in( 'titulopendente', 'pagamento', 'recebimento', 'resumotitulo')
		and exists( select 1
			from parametrocache
			where CacheId = c.Id
			and nome = 'empresaId'
			and valor = @empresaId )

		fetch cur into @empresaId
	end
	close cur
	deallocate cur
	---------------------------------------
	declare cur cursor for
	select distinct d.EmpresaId
	from deleted as d

	open cur
	fetch cur into @empresaId
	while @@FETCH_STATUS = 0
	begin
		delete cache 
		from cache c
		where Nome in( 'titulopendente', 'pagamento', 'recebimento', 'resumotitulo')
		and exists( select 1
			from parametrocache
			where CacheId = c.Id
			and nome = 'empresaId'
			and valor = @empresaId )

		fetch cur into @empresaId
	end
	close cur
	deallocate cur
end
GO

if object_id('tr_InvalidaCacheCategoria') > 0
begin
   print '<< CREATE tr_InvalidaCacheCategoria >>'
end
GO

	
