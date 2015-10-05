if object_id('pr_extrato') > 0
begin
   drop procedure pr_extrato
   print '<< DROP pr_extrato >>'
end

GO

create procedure pr_extrato (@empresa int, @conta int, @dataInicial smalldatetime , @dataFinal smalldatetime)
as
begin

	--pegar saldo inicial e data inicial da conta em questão
	declare @saldoConta money
	declare @DataSaldoInicial smalldatetime
	declare @Saldo money
	declare @SaldoInicial money

	select @saldoConta = SaldoInicial, 
	@DataSaldoInicial = DataSaldoInicial
	from Conta 
	where id = @conta 
	and EmpresaId = @empresa

	--verificar oq teve de movimentações na conta antes da data inicial do extrato, se a data inicial da conta for antes

	if @dataSaldoInicial < @dataInicial
	begin

		--definir o saldo na data inicial do extrato
		select @SaldoInicial = @saldoConta + isnull(SUM(case c.Direcao when 0 then l.valor + isnull(l.JurosMulta, 0.0) else -( l.valor + isnull(l.JurosMulta, 0.0)) end), 0.0)
		from liquidacao l
		join titulo t on l.TituloId = t.Id
		join categoria c on t.CategoriaId = c.Id
		where data < @dataInicial 
		and data >= @dataSaldoInicial 
		and t.ContaId = @conta

		select @saldoInicial = @saldoInicial + isnull(SUM(case when ContaOrigemId = @conta then -valor when contaDestinoId = @conta then valor else 0.0 end), 0.0)
		from Transferencia	
		where data < @dataInicial
		and data >= @dataSaldoInicial 

	end
	else
	begin
		set @SaldoInicial = @saldoConta
	end

	--pegar as movimentações realizadas até a data final do extrato
	select Data, case c.Direcao when 0 then l.valor + isnull(l.JurosMulta, 0.0) else -(l.valor+isnull(l.JurosMulta, 0.0)) end Valor, t.Descricao, convert(money,null) Saldo 
	into #movimentacoes
	from Liquidacao l
	join titulo t on l.TituloId = t.Id
	join categoria c on t.CategoriaId = c.Id
	where Data >= @dataInicial
	and (@dataFinal is null or Data < @dataFinal+1)
	and t.ContaId = @conta
	and t.EmpresaId = @empresa
	union
	select Data, case when ContaOrigemId = @conta then -valor when contaDestinoId = @conta then valor else 0.0 end Valor, Descricao, null
	from transferencia
	where Data >= @dataInicial
	and (@dataFinal is null or Data < @dataFinal+1)
	and EmpresaId = @empresa

	--calcular saldos por movimentação
	declare @valor money
	declare @acumulador money

	declare curSaldo cursor for 
	select valor
	from #movimentacoes
	for update of saldo

	set @acumulador = @saldoInicial

	OPEN curSaldo
	fetch curSaldo into @valor
	while @@FETCH_STATUS = 0
	begin
		set @acumulador = @acumulador + @valor

		update #movimentacoes
		set Saldo = @acumulador 
		where current of curSaldo

		fetch curSaldo into @valor

	end
	close curSaldo
	deallocate curSaldo

	insert into #movimentacoes
	values
	(@dataInicial-1, null, 'Saldo Inicial', @SaldoInicial)

	--retornar uma lista com as movimentações
	select *
	from #movimentacoes
	order by Data


	select @saldo = @SaldoInicial + SUM(Valor)
	from #movimentacoes



end

GO

if object_id('pr_extrato') > 0
begin
   print '<< CREATE pr_extrato >>'
end
GO

