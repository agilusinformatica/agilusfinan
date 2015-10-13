if object_id('pr_cria_titulo_virtual') > 0
begin
   drop procedure pr_cria_titulo_virtual
   print '<< DROP pr_cria_titulo_virtual >>'
end

GO

create procedure pr_cria_titulo_virtual(@id_empresa int, @data_inicial_analise smalldatetime, @data_final_analise smalldatetime)
/*----------------------------------------------------------------------------------------------------------------------
NOME: pr_cria_titulo_virtual
OBJETIVO: Criação de títulos virtuais
DATA: 22/07/2015
----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------*/
as
Begin
	declare @titulo_virtual table(
	TituloRecorrenteId int,
	nome varchar(100),
	DataVencimento datetime, 
	Valor money, 
	CategoriaId int, 
	PessoaId int, 
	CentroCustoId int,
	EmpresaId int)

	declare 
		@id int,
		@nome varchar(100),
		@dia_vencimento tinyint,
		@valor money,
		@recorrencia tinyint,
		@qtde_parcelas int,
		@categoria_id int,
		@pessoa_id int,
		@centro_custo_id int,
		@data_cadastro datetime,
		@direcao_vencimento tinyint

	declare cur cursor for
	select t.Id, t.nome, t.diaVencimento, t.valor, t.recorrencia, t.QtdeParcelas, t.CategoriaId, t.PessoaId, t.CentroCustoId, t.DataCadastro, c.DirecaoVencimentoDiaNaoUtil
	from TituloRecorrente as t
	join Categoria as c on t.CategoriaId = c.Id
	where t.EmpresaId = @id_empresa
	and ativo = 1

	open cur
	Fetch cur into @id, @nome, @dia_vencimento, @valor, @recorrencia, @qtde_parcelas, @categoria_id, @pessoa_id, @centro_custo_id, @data_cadastro, @direcao_vencimento
	While @@FETCH_STATUS = 0
	begin
		insert into @titulo_virtual
		select @id, @nome, vencimento.*, @valor, @categoria_id, @pessoa_id, @centro_custo_id, @id_empresa
		from dbo.fn_gerador_vencimentos(@id, @data_inicial_analise, @data_final_analise, @dia_vencimento, @qtde_parcelas, @data_cadastro, @recorrencia, @direcao_vencimento) as vencimento

		Fetch cur into @id, @nome, @dia_vencimento, @valor, @recorrencia, @qtde_parcelas, @categoria_id, @pessoa_id, @centro_custo_id, @data_cadastro, @direcao_vencimento
	end
	close cur
	deallocate cur

	select tv.TituloRecorrenteId, tv.nome, tv.DataVencimento, tv.Valor, tv.CategoriaId,	tv.PessoaId, tv.CentroCustoId, null as TituloId
	from @titulo_virtual as tv
	where not exists(select 1
					from Titulo as T
					where tv.TituloRecorrenteId = T.TituloRecorrenteId 
					and convert(date,tv.DataVencimento) = convert(date,T.DataVencimento))
	union

	select TituloRecorrenteId, Descricao, DataVencimento, Valor, CategoriaId, PessoaId, CentroCustoId, Id
	from Titulo as T
	where DataVencimento >= @data_inicial_analise
	and DataVencimento < @data_final_analise+1
	and empresaId = @id_empresa
	order by DataVencimento
end

GO

if object_id('pr_cria_titulo_virtual') > 0
begin
   print '<< CREATE pr_cria_titulo_virtual >>'
end
GO