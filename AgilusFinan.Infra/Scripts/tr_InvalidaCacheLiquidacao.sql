if object_id('tr_InvalidaCacheLiquidacao') > 0
begin
   drop trigger tr_InvalidaCacheLiquidacao
   print '<< DROP tr_InvalidaCacheLiquidacao >>'
end

GO

CREATE trigger tr_InvalidaCacheLiquidacao on Liquidacao for INSERT, UPDATE, DELETE as
/*----------------------------------------------------------------------------------------------------------------------
NOME: tr_InvalidaCacheLiquidacao
OBJETIVO: Invalidar cache de liquidacao
DATA: 18/03/2016
OBSERVAÇÃO: Para esta trigger funcionar é necessário criação das procs: prInvalidaCacheTituloPendente e prInvalidaCacheSaldo
Os arquivos de criação estão localizados na pasta Scripts (Projeto Infra).
----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------*/
begin
	declare @EmpresaId int, @Data datetime, @ContaId int
	declare cur cursor for
	select EmpresaId, Data, t.contaId
	from inserted i
	join titulo t on i.TituloId = t.Id

	open cur
	fetch cur into @empresaId, @Data, @ContaId
	while @@FETCH_STATUS = 0
	begin
		exec prInvalidaCacheFluxoCaixa @empresaId, @Data
		exec prInvalidaCachePrevistoRealizado @empresaId, @Data
		exec prInvalidaCacheTituloPendente @empresaId, @Data
		exec prInvalidaCacheSaldo @empresaId, @Data
		exec prInvalidaCacheExtrato @ContaId, @Data
		fetch cur into @empresaId, @Data, @ContaId
	end
	close cur
	deallocate cur
	---------------------------------------
	declare cur cursor for
	select EmpresaId, Data, t.contaId
	from deleted d
	join titulo t on d.TituloId = t.Id

	open cur
	fetch cur into @empresaId, @Data, @ContaId
	while @@FETCH_STATUS = 0
	begin
		exec prInvalidaCacheFluxoCaixa @empresaId, @Data
		exec prInvalidaCachePrevistoRealizado @empresaId, @Data
		exec prInvalidaCacheTituloPendente @empresaId, @Data
		exec prInvalidaCacheSaldo @empresaId, @Data
		exec prInvalidaCacheExtrato @ContaId, @Data
		fetch cur into @empresaId, @Data, @ContaId
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

	



	

