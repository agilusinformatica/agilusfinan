if object_id('fn_gerador_vencimentos') > 0
begin
   drop function fn_gerador_vencimentos
   print '<< DROP fn_gerador_vencimentos >>'
end

GO

	create procedure extrato (@conta int, @dataInicial smalldatetime , @dataFinal smalldatetime )
	as
	begin

		drop table #temp

		declare @saldoInicial decimal
		declare @DataSaldoInicial smalldatetime

		set @saldoInicial = (select SaldoInicial from Conta where id = 1)
		set @DataSaldoInicial = (select DataSaldoInicial from conta where id = 1)

		select @saldoInicial, @DataSaldoInicial

		select data, valor+JurosMulta valor
		into #temp
		from liquidacao
		where data >= @DataSaldoInicial 
		union
		select data, valor
		from transferencia
		where data >= @DataSaldoInicial

		select * from #temp

	end


GO

if object_id('fn_gerador_vencimentos') > 0
begin
   print '<< CREATE fn_gerador_vencimentos >>'
end
GO