if object_id('pr_conciliar_titulos') > 0
begin
   drop procedure pr_conciliar_titulos
   print '<< DROP pr_conciliar_titulos >>'
end

GO

create procedure pr_conciliar_titulos(@titulos_sem_vinculo as dbo.TitulosSemVinculo Readonly, @titulos_nao_criados as dbo.TitulosNaoCriados Readonly, @EmpresaId int)
/*----------------------------------------------------------------------------------------------------------------------
NOME: pr_conciliar_titulos
OBJETIVO: Criar v�nculo de t�tulos com extratos na tabela liquida��o
DATA: 21/01/2016
OBSERVA��O: Para esta procedure funcionar � necess�rio cria��o dos tipos: dbo.TitulosSemVinculo e dbo.TitulosNaoCriados.
O arquivo de cria��o est� localizado na pasta config.
----------------------------------------------------------------------------------------------------------------------*/
as
begin
	declare @Acrescimo money,
			@DataVencimento smalldatetime,
			@Desconto money,
			@Descricao varchar(100),
			@CategoriaId int,
			@TituloId int,
			@TituloRecorrenteId int,
			@CentroCustoId int,
			@Observacao varchar(8000),
			@Competencia smalldatetime,
			@Valor money,
			@ContaId int,
			@PessoaId int,
			@DataLancamento smalldatetime,
			@ConciliacaoExtratoId int

	declare cur cursor for
		select Acrescimo, DataVencimento, Desconto, Descricao, CategoriaId, TituloId, TituloRecorrenteId, Valor, ContaId, PessoaId, DataLancamento, ConciliacaoExtratoId
		from @titulos_sem_vinculo

	open cur
		fetch cur into @Acrescimo, @DataVencimento, @Desconto, @Descricao, @CategoriaId, @TituloId, @TituloRecorrenteId, @Valor, @ContaId, @PessoaId, @DataLancamento, @ConciliacaoExtratoId
		while @@FETCH_STATUS = 0
		begin
			if @TituloId is null
			begin
				insert into titulo(DataVencimento, Descricao, CategoriaId, TituloRecorrenteId, Valor, ContaId, PessoaId, EmpresaId)
				values(@DataVencimento, @Descricao, @CategoriaId, @TituloRecorrenteId, @Valor, @ContaId, @PessoaId, @EmpresaId)

				insert into Liquidacao(TituloId, Data, JurosMulta, Desconto, Valor, ConciliacaoExtratoId)values(@@IDENTITY, @DataLancamento, isnull(@Acrescimo,0), isnull(@Desconto,0), @Valor, @ConciliacaoExtratoId)
			end
			else
				insert into Liquidacao(TituloId, Valor, JurosMulta, Desconto, ConciliacaoExtratoId)values(@TituloId, @valor,  isnull(@Acrescimo,0), isnull(@Desconto,0), @ConciliacaoExtratoId)

			fetch cur into @Acrescimo, @DataVencimento, @Desconto, @Descricao, @CategoriaId, @TituloId, @TituloRecorrenteId, @Valor, @ContaId, @PessoaId, @DataLancamento, @ConciliacaoExtratoId
		end
	close cur
	deallocate cur

	declare cur cursor for
		select Acrescimo, DataVencimento, Desconto, Descricao, CategoriaId, CentroCustoId, Observacao, Competencia, Valor, ContaId, PessoaId, DataLancamento, ConciliacaoExtratoId
		from @titulos_nao_criados
	open cur
		fetch cur into @Acrescimo, @DataVencimento, @Desconto, @Descricao, @CategoriaId, @CentroCustoId, @Observacao, @Competencia, @Valor, @ContaId, @PessoaId, @DataLancamento, @ConciliacaoExtratoId
		while @@FETCH_STATUS = 0
		begin
			insert into titulo(DataVencimento, Descricao, CategoriaId, CentroCustoId, Observacao, Competencia, Valor, ContaId, PessoaId, EmpresaId)
			values(@DataVencimento, @Descricao, @CategoriaId, @CentroCustoId, @Observacao, @Competencia, @Valor, @ContaId, @PessoaId, @EmpresaId)

			insert into Liquidacao(TituloId, valor, JurosMulta, Desconto, Data, ConciliacaoExtratoId)values(@@IDENTITY, @valor, @Acrescimo, @Desconto, @DataLancamento, @ConciliacaoExtratoId)
			
			fetch cur into @Acrescimo, @DataVencimento, @Desconto, @Descricao, @CategoriaId, @CentroCustoId, @Observacao, @Competencia, @Valor, @ContaId, @PessoaId, @DataLancamento, @ConciliacaoExtratoId
		end		
	close cur
	deallocate cur
end

GO

if object_id('pr_conciliar_titulos') > 0
begin
   print '<< CREATE pr_conciliar_titulos >>'
end
GO

