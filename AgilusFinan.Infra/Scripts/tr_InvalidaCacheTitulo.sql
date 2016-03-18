if object_id('tr_InvalidaCacheTitulo') > 0
begin
   drop trigger tr_InvalidaCacheTitulo
   print '<< DROP tr_InvalidaCacheTitulo >>'
end

GO

CREATE trigger tr_InvalidaCacheTitulo on titulo for INSERT, UPDATE, DELETE as
/*----------------------------------------------------------------------------------------------------------------------
NOME: tr_InvalidaCacheTitulo
OBJETIVO: Invalidar cache de titulo
DATA: 18/03/2016
OBSERVA��O: Para esta trigger funcionar � necess�rio cria��o da proc: prInvalidaCacheTituloPendente
O arquivo de cria��o est� localizado na pasta Scripts (Projeto Infra).
----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------*/
begin
	declare @EmpresaId int, @DataVencimento datetime
	declare cur cursor for
	select EmpresaId, DataVencimento
	from inserted

	open cur
	fetch cur into @empresaId, @DataVencimento
	while @@FETCH_STATUS = 0
	begin
		exec prInvalidaCacheTituloPendente @empresaId, @DataVencimento
		fetch cur into @empresaId, @DataVencimento
	end
	close cur
	deallocate cur
	---------------------------------------
	declare cur cursor for
	select EmpresaId, DataVencimento
	from deleted

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

if object_id('tr_InvalidaCacheTitulo') > 0
begin
   print '<< CREATE tr_InvalidaCacheTitulo >>'
end
GO

	

