if object_id('pr_fluxo_caixa') > 0
begin
   drop procedure pr_fluxo_caixa
   print '<< DROP pr_fluxo_caixa >>'
end

GO

create procedure pr_fluxo_caixa (@empresa int, @dataInicial smalldatetime , @dataFinal smalldatetime, @periodicidade varchar(50) = 'Mensal')
/*----------------------------------------------------------------------------------------------------------------------
NOME: pr_fluxo_caixa
OBJETIVO: Relatório de fluxo de caixa
DATA: 08/10/2015
----------------------------------------------------------------------------------------------------------------------
exec pr_fluxo_caixa 1, '2015-08-01', '2015-12-31', 'Mensal'
exec pr_fluxo_caixa 1, '2015-01-01', '2015-12-31', 'Anual'
exec pr_fluxo_caixa 1, '2015-01-01', '2015-12-31', 'Trimestral'
exec pr_fluxo_caixa 1, '2015-01-01', '2015-12-31', 'Bimestral'
----------------------------------------------------------------------------------------------------------------------*/
as
begin
	
	create table #fluxo_caixa (
		Periodo varchar(50),
		SaldoInicial decimal(18,2),
		Receitas decimal(18,2),
		Despesas decimal(18,2),
		LucroPrejuizo decimal(18,2),
		Acumulado decimal(18,2),
		Lucratividade float
	)

	if @periodicidade = 'Mensal'
	begin
		insert into #fluxo_caixa (periodo, Receitas, Despesas)
		select convert(varchar, MONTH(Data)) + '/'+ convert(varchar,YEAR(Data)), SUM(case when c.Direcao = 0 then l.Valor+isnull(l.JurosMulta,0.0) - isnull(l.Desconto,0.0) else 0 end), 
		SUM(case when c.Direcao = 1 then l.Valor+isnull(l.JurosMulta,0.0) - isnull(l.Desconto,0.0) else 0 end)  
		from Liquidacao l
		join titulo t on l.TituloId = t.Id
		join categoria c on t.CategoriaId = c.Id
		where t.EmpresaId = @empresa
		and l.Data >= @DataInicial
		and l.Data < @DataFinal+1
		group by MONTH(l.Data), YEAR(l.Data)
		order by YEAR(l.Data), MONTH(l.Data)
	end
	if @periodicidade = 'Anual'
	begin
		insert into #fluxo_caixa (periodo, Receitas, Despesas)
		select convert(varchar,YEAR(Data)), SUM(case when c.Direcao = 0 then l.Valor+isnull(l.JurosMulta,0.0) - isnull(l.Desconto,0.0) else 0 end), 
		SUM(case when c.Direcao = 1 then l.Valor+isnull(l.JurosMulta,0.0) - isnull(l.Desconto,0.0) else 0 end)  
		from Liquidacao l
		join titulo t on l.TituloId = t.Id
		join categoria c on t.CategoriaId = c.Id
		where t.EmpresaId = @empresa
		and l.Data >= @DataInicial
		and l.Data < @DataFinal+1
		group by YEAR(l.Data)
		order by YEAR(l.Data)
	end
	if @periodicidade = 'Trimestral'
	begin
		insert into #fluxo_caixa (periodo, Receitas, Despesas)
		select convert(varchar, DATEPART(QQ, Data)) + '/'+ convert(varchar,YEAR(Data)), SUM(case when c.Direcao = 0 then l.Valor+isnull(l.JurosMulta,0.0) - isnull(l.Desconto,0.0) else 0 end), 
		SUM(case when c.Direcao = 1 then l.Valor+isnull(l.JurosMulta,0.0) - isnull(l.Desconto,0.0) else 0 end)  
		from Liquidacao l
		join titulo t on l.TituloId = t.Id
		join categoria c on t.CategoriaId = c.Id
		where t.EmpresaId = @empresa
		and l.Data >= @DataInicial
		and l.Data < @DataFinal+1
		group by DATEPART(QQ, l.Data), YEAR(l.Data)
		order by YEAR(l.Data), DATEPART(QQ,l.Data)
	end
	if @periodicidade = 'Bimestral'
	begin
		insert into #fluxo_caixa (periodo, Receitas, Despesas)
		select convert(varchar, ceiling(MONTH(l.Data)/2.0)) + '/'+ convert(varchar,YEAR(Data)), SUM(case when c.Direcao = 0 then l.Valor+isnull(l.JurosMulta,0.0) - isnull(l.Desconto,0.0) else 0 end), 
		SUM(case when c.Direcao = 1 then l.Valor+isnull(l.JurosMulta,0.0) - isnull(l.Desconto,0.0) else 0 end)  
		from Liquidacao l
		join titulo t on l.TituloId = t.Id
		join categoria c on t.CategoriaId = c.Id
		where t.EmpresaId = @empresa
		and l.Data >= @DataInicial
		and l.Data < @DataFinal+1
		group by ceiling(MONTH(l.Data)/2.0), YEAR(l.Data)
		order by YEAR(l.Data), ceiling(MONTH(l.Data)/2.0)
	end
	if @periodicidade = 'Semestral'
	begin
		insert into #fluxo_caixa (periodo, Receitas, Despesas)
		select convert(varchar, ceiling(MONTH(l.Data)/6.0)) + '/'+ convert(varchar,YEAR(Data)), SUM(case when c.Direcao = 0 then l.Valor+isnull(l.JurosMulta,0.0) - isnull(l.Desconto,0.0) else 0 end), 
		SUM(case when c.Direcao = 1 then l.Valor+isnull(l.JurosMulta,0.0) - isnull(l.Desconto,0.0) else 0 end)  
		from Liquidacao l
		join titulo t on l.TituloId = t.Id
		join categoria c on t.CategoriaId = c.Id
		where t.EmpresaId = @empresa
		and l.Data >= @DataInicial
		and l.Data < @DataFinal+1
		group by ceiling(MONTH(l.Data)/6.0), YEAR(l.Data)
		order by YEAR(l.Data), ceiling(MONTH(l.Data)/6.0)
	end

	declare @SaldoInicial money,
		    @LucroPrejuizo money,
			@Acumulado money,
			@Lucratividade float,
			@Receitas money,
			@Despesas money

	declare curFluxo cursor for 
	select Receitas, Despesas
	from #fluxo_caixa

	open curFluxo
	fetch curFluxo into @Receitas, @Despesas
	
	set @SaldoInicial = dbo.fn_saldo(null, @dataInicial, @empresa)

	while @@FETCH_STATUS = 0
	begin
		set @LucroPrejuizo = @Receitas - @Despesas
		set @Acumulado = @SaldoInicial + @LucroPrejuizo
		if @Receitas <> 0
		   set @Lucratividade = (@LucroPrejuizo / @Receitas) * 100.0
		else
		   set @Lucratividade = 0

		update #fluxo_caixa
		set SaldoInicial = @SaldoInicial,
		LucroPrejuizo = @LucroPrejuizo,
		Acumulado = @Acumulado,
		Lucratividade =  @Lucratividade
		where current of curFluxo

		set @SaldoInicial = @Acumulado

		fetch curFluxo into @Receitas, @Despesas
	end

	select *
	from #fluxo_caixa

end

GO


if object_id('pr_fluxo_caixa') > 0
begin
   print '<< CREATE pr_fluxo_caixa >>'
end
GO
