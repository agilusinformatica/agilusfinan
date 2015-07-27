if object_id('pr_cria_titulo_virtual') > 0
begin
   drop procedure pr_cria_titulo_virtual
   print '<< DROP pr_cria_titulo_virtual >>'
end

GO

create procedure pr_cria_titulo_virtual(@id_empresa int, @data_inicial_analise smalldatetime, @data_final_analise smalldatetime)
/*----------------------------------------------------------------------------------------------------------------------
NOME: pr_cria_titulo_virtual
OBJETIVO: Cria��o de t�tulos virtuais
DATA: 22/07/2015
----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------*/
with encryption
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

	declare @id int,
	@nome varchar(100),
	@dia_vencimento tinyint,
	@valor money,
	@recorrencia tinyint,
	@qtde_parcelas int,
	@categoria_id int,
	@pessoa_id int,
	@centro_custo_id int,
	@data_cadastro datetime

	declare cur cursor for
	select id, nome, diaVencimento, valor, recorrencia, QtdeParcelas, CategoriaId, PessoaId, CentroCustoId, DataCadastro
	from TituloRecorrente
	where EmpresaId = @id_empresa
	and ativo = 1

	open cur
	Fetch cur into @id, @nome, @dia_vencimento, @valor, @recorrencia, @qtde_parcelas, @categoria_id, @pessoa_id, @centro_custo_id, @data_cadastro
	While @@FETCH_STATUS = 0
	begin
		insert into @titulo_virtual
		select @id, @nome, vencimento.*, @valor, @categoria_id, @pessoa_id, @centro_custo_id, @id_empresa
		from dbo.fn_gerador_vencimentos(@id, @data_inicial_analise, @data_final_analise, @dia_vencimento, @qtde_parcelas, @data_cadastro, @recorrencia) as vencimento

		Fetch cur into @id, @nome, @dia_vencimento, @valor, @recorrencia, @qtde_parcelas, @categoria_id, @pessoa_id, @centro_custo_id, @data_cadastro
	end
	close cur
	deallocate cur

	select tv.TituloRecorrenteId, tv.nome, tv.DataVencimento, tv.Valor, tv.CategoriaId,	tv.PessoaId, tv.CentroCustoId, t.id as IdTitulo
	from @titulo_virtual as tv
	left join Titulo as t on t.TituloRecorrenteId = tv.TituloRecorrenteId and tv.DataVencimento = t.DataVencimento
	union

	select TituloRecorrenteId, Descricao, DataVencimento, Valor, CategoriaId, PessoaId, CentroCustoId, Id
	from Titulo as T
	where not exists(select sum(valor) as valor
					from Liquidacao as L
					where T.Id = L.TituloId
					group by L.TituloId
					having sum(valor) = T.valor)
	order by DataVencimento
end

GO

if object_id('pr_cria_titulo_virtual') > 0
begin
   print '<< CREATE pr_cria_titulo_virtual >>'
end
GO