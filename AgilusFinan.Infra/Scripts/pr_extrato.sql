if object_id('pr_extrato') > 0
begin
   drop procedure pr_extrato
   print '<< DROP pr_extrato >>'
end
GO

--exec pr_extrato 1, 1, '2016-01-01', '2016-12-15'


create procedure pr_extrato (@empresa int, @conta int, @dataInicial smalldatetime , @dataFinal smalldatetime)
as
begin

	--pegar saldo inicial e data inicial da conta em questão
	declare @DataSaldoInicial smalldatetime
	declare @SaldoInicial money
    declare @LiquidacaoId int
    declare @TransferenciaId int

	select @DataSaldoInicial = DataSaldoInicial
	from Conta 
	where id = @conta 
	and EmpresaId = @empresa

	set @saldoInicial = dbo.fn_saldo (@conta, @dataInicial-1, @empresa)

	create table #movimentacoes(Data datetime, LiquidacaoId int, TransferenciaId int, Valor decimal(18,2), Descricao varchar(100), Categoria varchar(100), DataVencimento datetime, Saldo money)

	--pegar as movimentações realizadas até a data final do extrato
	insert into #movimentacoes
	select Data, l.Id LiquidacaoId, null TransferenciaId,
      case c.Direcao 
         when 0 then l.valor + isnull(l.JurosMulta, 0.0) - isnull(l.Desconto,0.0)
         else -(l.valor+isnull(l.JurosMulta,0.0) - isnull(l.Desconto,0.0)) 
      end Valor, 
      t.Descricao, c.Nome Categoria, t.DataVencimento DataVencimento, convert(money,null) Saldo 
	from Liquidacao l
	join titulo t on l.TituloId = t.Id
	join categoria c on t.CategoriaId = c.Id
	where Data >= @dataInicial
	and Data >= @DataSaldoInicial
	and (@dataFinal is null or Data < @dataFinal+1)
	and t.ContaId = @conta
	and t.EmpresaId = @empresa

	union all

	select Data, null, Id TransferenciaId,
      case 
         when ContaOrigemId = @conta then -valor 
         when contaDestinoId = @conta then valor 
         else 0.0 
      end Valor, 
      Descricao, null, null, null
	from transferencia
	where Data >= @dataInicial
	and Data >= @DataSaldoInicial
	and (@dataFinal is null or Data < @dataFinal+1)
	and EmpresaId = @empresa

	--calcular saldos por movimentação
	declare @valor money
	declare @acumulador money

	declare curSaldo cursor for 
	select valor, LiquidacaoId, TransferenciaId
	from #movimentacoes
    order by data, LiquidacaoId, TransferenciaId

	set @acumulador = @saldoInicial

	OPEN curSaldo
	fetch curSaldo into @valor, @LiquidacaoId, @TransferenciaId
	while @@FETCH_STATUS = 0
	begin
		set @acumulador = @acumulador + @valor

		update #movimentacoes
		set Saldo = @acumulador 
		where (@LiquidacaoId is null or LiquidacaoId = @LiquidacaoId)
        and (@TransferenciaId is null or TransferenciaId = @TransferenciaId)

		fetch curSaldo into @valor, @LiquidacaoId, @TransferenciaId

	end
	close curSaldo
	deallocate curSaldo

	insert into #movimentacoes
	values (@dataInicial-1, null, null, null, 'Saldo Inicial', null, null, @SaldoInicial)

	--retornar uma lista com as movimentações
	select *
	from #movimentacoes
	order by Data, LiquidacaoId, TransferenciaId

	drop table #movimentacoes
end
GO

if object_id('pr_extrato') > 0
begin
   print '<< CREATE pr_extrato >>'
end
GO