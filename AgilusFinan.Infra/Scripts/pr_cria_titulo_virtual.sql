create procedure pr_cria_titulo_virtual(@id_empresa int, @data_inicial_analise smalldatetime, @data_final_analise smalldatetime) as
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

	select *
	from @titulo_virtual
end



