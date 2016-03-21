if object_id('tr_InvalidaCacheTituloRecorrente') > 0
begin
   drop trigger tr_InvalidaCacheTituloRecorrente
   print '<< DROP tr_InvalidaCacheTituloRecorrente >>'
end

GO

CREATE trigger tr_InvalidaCacheTituloRecorrente on TituloRecorrente for INSERT, UPDATE, DELETE as
/*----------------------------------------------------------------------------------------------------------------------
NOME: tr_InvalidaCacheTituloRecorrente
OBJETIVO: Invalidar cache de Titulo Recorrente
DATA: 21/03/2016
OBSERVA��O: Para esta trigger funcionar � necess�rio cria��o das procs: prInvalidaCacheTituloPendente e prInvalidaCachePrevistoRealizado
Os arquivos de cria��o est�o localizados na pasta Scripts (Projeto Infra).
----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------*/
begin
	declare @EmpresaId int
	declare cur cursor for
	select distinct EmpresaId
	from inserted i

	open cur
	fetch cur into @empresaId
	while @@FETCH_STATUS = 0
	begin
		delete cache
		from cache c
		where Nome in ('resumotitulo', 'previstorealizado', 'titulopendente')
		and exists (select 1
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
	select distinct EmpresaId
	from deleted i

	open cur
	fetch cur into @empresaId
	while @@FETCH_STATUS = 0
	begin
		delete cache
		from cache c
		where Nome in ('resumotitulo', 'previstorealizado', 'titulopendente')
		and exists (select 1
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

if object_id('tr_InvalidaCacheTituloRecorrente') > 0
begin
   print '<< CREATE tr_InvalidaCacheTituloRecorrente >>'
end
GO

	



	

