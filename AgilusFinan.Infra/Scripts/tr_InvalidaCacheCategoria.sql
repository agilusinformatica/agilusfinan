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
OBSERVAÇÃO: Para esta trigger funcionar é necessário criação das procs: prInvalidaCacheTituloPendente e prInvalidaCacheSaldo
Os arquivos de criação estão localizados na pasta Scripts (Projeto Infra).
----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------*/
begin
	declare @EmpresaId int, @DataVencimento datetime
	declare cur cursor for
	select EmpresaId, Data
	from inserted i
	join titulo t on i.TituloId = t.Id

	open cur
	fetch cur into @empresaId, @DataVencimento
	while @@FETCH_STATUS = 0
	begin
		exec prInvalidaCacheTituloPendente @empresaId, @DataVencimento
		exec prInvalidaCacheSaldo @empresaId, @DataVencimento
		fetch cur into @empresaId, @DataVencimento
	end
	close cur
	deallocate cur
	---------------------------------------
	declare cur cursor for
	select EmpresaId, Data
	from deleted d
	join titulo t on d.TituloId = t.Id

	open cur
	fetch cur into @empresaId, @DataVencimento
	while @@FETCH_STATUS = 0
	begin
		exec prInvalidaCacheTituloPendente @empresaId, @DataVencimento
		fetch cur into @empresaId, @DataVencimento
	end
	close cur
	deallocate cur
end
GO

if object_id('tr_InvalidaCacheLiquidacao') > 0
begin
   print '<< CREATE tr_InvalidaCacheLiquidacao >>'
end
GO

	



	

