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
OBSERVAÇÃO: Para esta trigger funcionar é necessário criação da proc: prInvalidaCacheTituloPendente
O arquivo de criação está localizado na pasta Scripts (Projeto Infra).
----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------*/
begin
	declare @EmpresaId int, @DataVencimento datetime, @Direcao int
	declare cur cursor for
	select i.EmpresaId, DataVencimento, c.Direcao
	from inserted as i
	inner join categoria as c on c.Id = i.CategoriaId

	open cur
	fetch cur into @empresaId, @DataVencimento, @Direcao
	while @@FETCH_STATUS = 0
	begin
		exec prInvalidaCacheTituloPendente @empresaId, @DataVencimento
		if @Direcao = 0
			exec prInvalidaCacheRecebimento @empresaId, @DataVencimento
		else
			exec prInvalidaCachePagamento @empresaId, @DataVencimento
		fetch cur into @empresaId, @DataVencimento, @Direcao
	end
	close cur
	deallocate cur
	---------------------------------------
	declare cur cursor for
	select d.EmpresaId, DataVencimento, c.Direcao
	from deleted as d
	inner join categoria as c on c.Id = d.CategoriaId

	open cur
	fetch cur into @empresaId, @DataVencimento, @Direcao
	while @@FETCH_STATUS = 0
	begin
		exec prInvalidaCacheTituloPendente @empresaId, @DataVencimento
		if @Direcao = 0
			exec prInvalidaCacheRecebimento @empresaId, @DataVencimento
		else
			exec prInvalidaCachePagamento @empresaId, @DataVencimento
		fetch cur into @empresaId, @DataVencimento, @Direcao
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

	

