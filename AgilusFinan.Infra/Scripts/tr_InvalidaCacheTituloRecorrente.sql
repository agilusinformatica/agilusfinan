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
OBSERVAÇÃO: Para esta trigger funcionar é necessário criação das procs: prInvalidaCacheTituloPendente e prInvalidaCachePrevistoRealizado
Os arquivos de criação estão localizados na pasta Scripts (Projeto Infra).
----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------*/
begin
	declare 
		@EmpresaId int,
		@DataCadastro datetime

	declare cur cursor for
	select EmpresaId, DataCadastro
	from inserted i

	open cur

	fetch cur into @empresaId, @DataCadastro

	while @@FETCH_STATUS = 0
	begin
		delete cache
		from cache c
		where Nome in ('resumotitulo', 'previstorealizado', 'titulopendente', 'pagamento', 'recebimento')
		and exists (select 1
					from parametrocache
					where CacheId = c.Id
					and nome = 'empresaId'
					and valor = @empresaId )
		and exists (select 1
		            from parametroCache
					where CacheId = c.Id
					and nome = 'dataInicial'
					and @DataCadastro >= convert(datetime,valor,103)  )

		fetch cur into @empresaId, @DataCadastro
	end
	close cur
	deallocate cur
	---------------------------------------
	declare cur cursor for
	select distinct EmpresaId, DataCadastro
	from deleted i

	open cur

	fetch cur into @empresaId, @DataCadastro

	while @@FETCH_STATUS = 0
	begin
		delete cache
		from cache c
		where Nome in ('resumotitulo', 'previstorealizado', 'titulopendente', 'pagamento', 'recebimento')
		and exists (select 1
					from parametrocache
					where CacheId = c.Id
					and nome = 'empresaId'
					and valor = @empresaId )
		and exists (select 1
		            from parametroCache
					where CacheId = c.Id
					and nome = 'dataInicial'
					and @DataCadastro >= convert(datetime,valor,103)  )

		fetch cur into @empresaId, @DataCadastro
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

	



	

