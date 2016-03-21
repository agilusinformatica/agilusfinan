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
	declare @EmpresaId int, @Data datetime, @ContaOrigemId int, @ContaDestinoId int
	declare cur cursor for
	select EmpresaId, Data, ContaOrigemId, ContaDestinoId
	from inserted

	open cur
	fetch cur into @empresaId, @Data, @ContaOrigemId, @ContaDestinoId
	while @@FETCH_STATUS = 0
	begin
		exec prInvalidaCacheSaldo @empresaId, @Data
		exec prInvalidaCacheExtrato @ContaOrigemId, @Data
		exec prInvalidaCacheExtrato @ContaDestinoId, @Data
		fetch cur into @empresaId, @Data, @ContaOrigemId, @ContaDestinoId
	end
	close cur
	deallocate cur
	---------------------------------------
	declare cur cursor for
	select EmpresaId, Data, ContaOrigemId, ContaDestinoId
	from deleted

	open cur
	fetch cur into @empresaId, @Data, @ContaOrigemId, @ContaDestinoId
	while @@FETCH_STATUS = 0
	begin
		exec prInvalidaCacheSaldo @empresaId, @Data
		exec prInvalidaCacheExtrato @ContaOrigemId, @Data
		exec prInvalidaCacheExtrato @ContaDestinoId, @Data
		fetch cur into @empresaId, @Data, @ContaOrigemId, @ContaDestinoId
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

	



