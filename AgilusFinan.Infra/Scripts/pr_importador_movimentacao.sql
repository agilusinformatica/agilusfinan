if object_id('pr_importador_movimentacao') > 0
begin
   drop procedure pr_importador_movimentacao
   print '<< DROP pr_importador_movimentacao >>'
end

GO

create procedure pr_importador_movimentacao(@EmpresaId smallint)
/*----------------------------------------------------------------------------------------------------------------------
NOME: pr_importador_movimentacao
OBJETIVO: Importaçao de dados do 'Zero Paper'
DATA: 30/11/2015
----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------*/
as
begin
	declare @TipoLancamento varchar(255),
			@DataPagamento smalldatetime,
			@Descricao varchar(255),
			@Valor money,
			@Categoria varchar(100),
			@Pessoa varchar(255),
			@Pago varchar(10),
			@Conta varchar(255),
			@Direcao smallint,
			@TotalCategoria int,
			@TotalConta int,
			@PessoaId int,
			@CategoriaId int,
			@ContaId int,
			@ContaOrigem int,
			@TituloId int
	set nocount on
	--Inclusao de pesssoas nao cadastradas no sistema
	select distinct [Recebido de/Pago a] into #Pessoas from movimentacoes
	
	insert into Pessoa(EmpresaId, Nome, ContaBancaria_Poupanca, DataNascimento)
	select @EmpresaId, [Recebido de/Pago a], 0, '20150101' from #Pessoas
	where not exists(select 1 from Pessoa
					 where Nome = [Recebido de/Pago a])
	and [Recebido de/Pago a] is not null

	--Declaracao do cursor
	declare curMovimentacao cursor for
	select [Tipo de Lancamento], [Data Pagamento], Descricao, Valor, Categoria, [Recebido de/Pago a], Pago, Conta
	from movimentacoes

	open curMovimentacao
	fetch curMovimentacao into @TipoLancamento, @DataPagamento, @Descricao, @Valor, @Categoria, @Pessoa, @Pago, @Conta
	
	while @@FETCH_STATUS = 0
	begin

		--O Id cadastrado no AgilusFinan da planilha a ser importada
		set @PessoaId = (select Id from Pessoa
						 where  Nome = @Pessoa)

		--Id da conta no AgilusFinan
		set @ContaId = (select Id from Conta
						where  Nome = @Conta)

		if (@ContaId is null)
		begin
			insert into Conta(Nome, SaldoInicial, BancoBoletoId, EmpresaId, Padrao)
			select @Conta, 0.0, 1, @EmpresaId, 0
		end


		if(@TipoLancamento like 'Trans%')
		begin
			set @ContaOrigem = (select Id from Conta
							    where Nome = @Descricao)

			if(@ContaOrigem is null)
			begin
				set @ContaOrigem = 1
			end

			insert into Transferencia(ContaOrigemId, ContaDestinoId, Valor, Data, Descricao, EmpresaId)
			select @ContaOrigem, @ContaId, @Valor, @DataPagamento, null, @EmpresaId
			where not exists(select 1 from Transferencia
							 where @ContaOrigem = ContaOrigemId
							 and @ContaId = ContaDestinoId
							 and @Valor = Valor
							 and @DataPagamento = Data)
							 
		end
		else
		begin
		--Verifica se @Categoria ja estao cadastrada na tabela Categoria
			set @CategoriaId = (select Id from Categoria
								where Nome = @Categoria)

		--Se @TotalContador retornar '0', significa que sera necessario cadastrar a categoria.
			if(@CategoriaId is null)
			begin
		
				set @Direcao = (case
							    when @TipoLancamento like 'Recebimento%' then 0 
							    else 1 
							    end)
				
				insert into Categoria(Nome, Direcao, Cor, EmpresaId, DirecaoVencimentoDiaNaoUtil)
				select @Categoria, @Direcao , 0, @EmpresaId, 0
				set @CategoriaId =  SCOPE_IDENTITY()

			end

			--Insercao do titulo
			insert into Titulo(ContaId, DataVencimento, Descricao, Valor, CategoriaId, PessoaId, EmpresaId)
			select @ContaId, @DataPagamento, @Descricao, @Valor, @CategoriaId, @PessoaId, @EmpresaId
		
			set @TituloId = SCOPE_IDENTITY()			
			print 'Titulo: ' + convert(varchar, @TituloId)

			--Liquidacao
			If(@Pago like 'Sim')
			begin
				print 'Liquidacao: ' + convert(varchar, @TituloId)
				insert into Liquidacao(Data, Valor, JurosMulta, FormaLiquidacao, TituloId)
				select @DataPagamento, @Valor, 0.0, 1, @TituloId

			end		
							
		end

		--Fim
		fetch curMovimentacao into @TipoLancamento, @DataPagamento, @Descricao, @Valor, @Categoria, @Pessoa, @Pago, @Conta
	
	end

	close curMovimentacao
	deallocate curMovimentacao

end

GO

if object_id('pr_importador_movimentacao') > 0
begin
   print '<< CREATE pr_importador_movimentacao >>'
end
GO

exec pr_importador_movimentacao 1

delete Liquidacao
delete Titulo
delete Transferencia
delete TituloRecorrente
delete Categoria
delete Pessoa

select distinct [Tipo de Lancamento] from movimentacoes
select distinct [Recebido de/Pago a] from movimentacoes

select * from Titulo
select * from Liquidacao
select * from Categoria
select * from pessoa
select * from titulo
select * from movimentacoes
select * from Transferencia
select * from Liquidacao
select * from conta


select distinct pago from movimentacoes

select * from movimentacoes
where [Tipo de Lancamento] like '%Trans%'

with dps as(
			select *, ROW_NUMBER() over(partition by [Data Pagamento], Descricao, Valor, [Recebido de/Pago a], Conta order by Conta) linha
			from movimentacoes
			)
select * from dps
where linha > 1




select * from Titulo
where id = 73528