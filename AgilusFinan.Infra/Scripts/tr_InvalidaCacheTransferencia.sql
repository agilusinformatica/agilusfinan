if object_id('tr_InvalidaCacheTransferencia') > 0
begin
   drop trigger tr_InvalidaCacheTransferencia
   print '<< DROP tr_InvalidaCacheTransferencia >>'
end

GO

create trigger tr_InvalidaCacheTransferencia on Transferencia for INSERT, UPDATE, DELETE as
/*----------------------------------------------------------------------------------------------------------------------
NOME: tr_InvalidaCacheTransferencia
OBJETIVO: Invalidar cache de Transferencia
DATA: 18/03/2016
OBSERVAÇÃO: Para esta trigger funcionar é necessário criação da proc: prInvalidaCacheSaldo
O arquivo de criação está localizado na pasta Scripts (Projeto Infra).
----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------*/
begin
	declare @EmpresaId int, @DataVencimento datetime
	declare cur cursor for
	select EmpresaId, Data
	from inserted

	open cur
	fetch cur into @empresaId, @DataVencimento
	while @@FETCH_STATUS = 0
	begin
		exec prInvalidaCacheSaldo @empresaId, @DataVencimento
		fetch cur into @empresaId, @DataVencimento
	end
	close cur
	deallocate cur
	---------------------------------------
	declare cur cursor for
	select EmpresaId, Data
	from deleted

	open cur
	fetch cur into @empresaId, @DataVencimento
	while @@FETCH_STATUS = 0
	begin
		exec prInvalidaCacheSaldo @empresaId, @DataVencimento
		fetch cur into @empresaId, @DataVencimento
	end
	close cur
	deallocate cur
end

GO

if object_id('tr_InvalidaCacheTransferencia') > 0
begin
   print '<< CREATE tr_InvalidaCacheTransferencia >>'
end
GO

	



