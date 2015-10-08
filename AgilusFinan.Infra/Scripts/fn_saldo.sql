if object_id('fn_saldo') > 0
begin
   drop function fn_saldo
   print '<< DROP fn_saldo >>'
end

GO

create function fn_saldo (@contaId int, @data smalldatetime)
RETURNS money
/*----------------------------------------------------------------------------------------------------------------------
NOME: fn_saldo
OBJETIVO: Define o saldo na data 
DATA: 08/10/2015
TESTES: 
select dbo.fn_saldo(1, '2015-09-03')
select dbo.fn_saldo(1, '2015-09-03')
----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------*/
begin
	--pegar saldo inicial e data inicial da conta em questão
	declare @saldoInicial money
	declare @DataSaldoInicial smalldatetime
	declare @Saldo money

	select 
		@saldoInicial = SaldoInicial, 
		@DataSaldoInicial = DataSaldoInicial
	from Conta 
	where id = @contaId 

	--verificar oq teve de movimentações na conta antes da data inicial do saldo, se a data inicial da conta for antes

	if @dataSaldoInicial < @data
	begin

		--definir o saldo na data 
		select @Saldo = @saldoInicial + isnull(SUM(case c.Direcao when 0 then l.valor + isnull(l.JurosMulta, 0.0) else -( l.valor + isnull(l.JurosMulta, 0.0)) end), 0.0)
		from liquidacao l
		join titulo t on l.TituloId = t.Id
		join categoria c on t.CategoriaId = c.Id
		where data <= @data 
		and data >= @dataSaldoInicial 
		and t.ContaId = @contaId

		select @saldo = @saldo + isnull(SUM(case when ContaOrigemId = @contaId then -valor when contaDestinoId = @contaId then valor else 0.0 end), 0.0)
		from Transferencia	
		where data <= @data
		and data >= @dataSaldoInicial 

	end
	else
	begin
		set @Saldo = @saldoInicial
	end

	return isnull(@Saldo,0)
end

GO

if object_id('fn_saldo') > 0
begin
   print '<< CREATE fn_saldo >>'
end
GO