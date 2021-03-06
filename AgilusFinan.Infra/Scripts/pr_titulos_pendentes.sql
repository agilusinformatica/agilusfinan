if object_id('pr_titulos_pendentes') > 0
begin
   drop procedure pr_titulos_pendentes
   print '<< DROP pr_titulos_pendentes >>'
end

GO

create procedure pr_titulos_pendentes(@id_empresa int, @data_inicial smalldatetime, @data_final smalldatetime, @tipo_pessoa int = null)
/*----------------------------------------------------------------------------------------------------------------------
NOME: pr_titulos_pendentes
OBJETIVO: Lista os t�tulos pendentes, reais ou virtuais
DATA: 22/07/2015
----------------------------------------------------------------------------------------------------------------------
exec pr_titulos_pendentes 2, '2015-07-01', '2015-08-31'
----------------------------------------------------------------------------------------------------------------------*/
as
Begin
	declare @titulos table(
	TituloRecorrenteId int,
	Descricao varchar(100),
	DataVencimento datetime, 
	Valor money, 
	CategoriaId int, 
	ContaId int,
	PessoaId int, 
	CentroCustoId int,
	TituloId int)

	insert into @titulos
	exec pr_cria_titulo_virtual @id_empresa, @data_inicial, @data_final

	select T.*, c.Nome as NomeCategoria, P.Nome as NomePessoa, cc.Nome as NomeCentroCusto, c.Direcao, co.Nome as NomeConta
	from @titulos as T
	inner join categoria as c on T.CategoriaId = c.Id
	inner join pessoa as p on T.PessoaId = p.Id
	inner join conta as co on T.ContaId = co.Id
	left join CentroCusto as cc on T.CentroCustoId = cc.Id
	where not exists(
		select sum(valor) as valor
		from Liquidacao as L
		where T.TituloId = L.TituloId
		group by L.TituloId
		having sum(valor) >= T.valor)
	and (@tipo_pessoa is null or exists( select 1 from tipoPessoaporPessoa tp where p.Id = tp.PessoaId and tp.TipoPessoaId = @tipo_pessoa))
end

GO

if object_id('pr_titulos_pendentes') > 0
begin
   print '<< CREATE pr_titulos_pendentes >>'
end
GO