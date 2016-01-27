if object_id('fn_saldo') > 0
begin
   drop function fn_saldo
   print '<< DROP fn_saldo >>'
end

GO

create function fn_saldo (@contaId int, @data smalldatetime, @empresaId int)
RETURNS money
/*----------------------------------------------------------------------------------------------------------------------
NOME: fn_saldo
OBJETIVO: Define o saldo na data 
DATA: 08/10/2015
TESTES: 
select dbo.fn_saldo(1, '2015-09-03', 1)
select dbo.fn_saldo(1, '2015-09-03')
----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------*/
begin
	--pegar saldo inicial e data inicial da conta em questão
	declare @saldoInicial money
	declare @DataSaldoInicial smalldatetime
	declare @Saldo money = 0
	declare @contaIdCursor int

	declare curSaldo cursor for
	select SaldoInicial, DataSaldoInicial, Id
	from Conta 
	where (@contaId is null or id = @contaId)
	and EmpresaId = @empresaId
	
	open curSaldo 
	fetch curSaldo into @saldoInicial, @DataSaldoInicial, @contaIdCursor

	while @@FETCH_STATUS = 0
	begin
		if @dataSaldoInicial < @data
		begin
			
			--definir o saldo na data 
			select @Saldo = @Saldo + @saldoInicial + isnull(SUM(case c.Direcao when 0 then l.valor + isnull(l.JurosMulta, 0.0) - isnull(l.Desconto, 0.0) else -( l.valor + isnull(l.JurosMulta, 0.0) - isnull(l.Desconto, 0.0)) end), 0.0)
			from liquidacao l
			join titulo t on l.TituloId = t.Id
			join categoria c on t.CategoriaId = c.Id
			where data < @data + 1
			and data >= @dataSaldoInicial 
			and t.ContaId = @contaIdCursor

			select @saldo = @saldo + isnull(SUM(case when ContaOrigemId = @contaIdCursor then -valor when contaDestinoId = @contaIdCursor then valor else 0.0 end), 0.0)
			from Transferencia	
			where data < @data + 1
			and data >= @dataSaldoInicial 
			and EmpresaId = @empresaId

		end
		else
		begin
			set @Saldo = @Saldo +  @saldoInicial
		end

		fetch curSaldo into @saldoInicial, @DataSaldoInicial, @contaIdCursor
	end

	close curSaldo
	deallocate curSaldo

	return isnull(@Saldo,0)
end

GO

if object_id('fn_saldo') > 0
begin
   print '<< CREATE fn_saldo >>'
end
GO
