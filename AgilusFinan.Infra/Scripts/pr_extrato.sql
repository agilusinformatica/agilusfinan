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
	declare @saldoInicial decimal
	declare @DataSaldoInicial smalldatetime
	declare @Saldo decimal

	select @saldoInicial = SaldoInicial, 
	@DataSaldoInicial = DataSaldoInicial
	from Conta 
	where id = @conta 
	and EmpresaId = @empresa


	--verificar oq teve de movimentações na conta antes da data inicial do extrato, se a data inicial da conta for antes

	if @dataSaldoInicial < @dataInicial
	begin

		--definir o saldo na data inicial do extrato
		select @saldo = @saldoInicial + SUM(case c.Direcao when 0 then l.valor + l.JurosMulta else -( l.valor + l.JurosMulta) end)
		from liquidacao l
		join titulo t on l.TituloId = t.Id
		join categoria c on t.CategoriaId = c.Id
		where data <= @DataSaldoInicial 
		and t.ContaId = @conta

		select @saldo = @saldo + SUM(case when ContaOrigemId = @conta then -valor when contaDestinoId = @conta then valor else 0 end)
		from Transferencia	
		where data <= @DataSaldoInicial
	end
	else
	begin
		set @saldo = @saldoInicial
	end

	--pegar as movimentações realizadas até a data final do extrato
	select Data, case c.Direcao when 0 then l.valor + l.JurosMulta else -(l.valor+JurosMulta) end Valor, t.Descricao
	into #movimentacoes
	from Liquidacao l
	join titulo t on l.TituloId = t.Id
	join categoria c on t.CategoriaId = c.Id
	where Data between @dataInicial and isnull(@dataFinal,getDate())
	and t.ContaId = @conta
	and t.EmpresaId = @empresa
	union
	select Data, case when ContaOrigemId = @conta then -valor when contaDestinoId = @conta then valor else 0 end Valor, Descricao
	from transferencia
	where Data between @dataInicial and isnull(@dataFinal,getDate())
	and EmpresaId = @empresa


	--retornar uma lista com as movimentações

	select *
	from #movimentacoes
	order by Data

	select @saldo = @saldo + SUM(Valor)
	from #movimentacoes

	select @saldo

end

GO

if object_id('pr_extrato') > 0
begin
   print '<< CREATE pr_extrato >>'
end
GO

--exec pr_extrato 1, 1, '2015-01-01', '2015-09-09'


