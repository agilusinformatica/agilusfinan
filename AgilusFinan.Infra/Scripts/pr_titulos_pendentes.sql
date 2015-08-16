if object_id('pr_titulos_pendentes') > 0
begin
   drop procedure pr_titulos_pendentes
   print '<< DROP pr_titulos_pendentes >>'
end

GO

create procedure pr_titulos_pendentes(@id_empresa int, @data_inicial smalldatetime, @data_final smalldatetime)
/*----------------------------------------------------------------------------------------------------------------------
NOME: pr_titulos_pendentes
OBJETIVO: Lista os títulos pendentes, reais ou virtuais
DATA: 22/07/2015
----------------------------------------------------------------------------------------------------------------------
exec pr_titulos_pendentes 2, '2015-07-01', '2015-08-31'
----------------------------------------------------------------------------------------------------------------------*/
with encryption
as
Begin
	declare @titulos table(
	TituloRecorrenteId int,
	Descricao varchar(100),
	DataVencimento datetime, 
	Valor money, 
	CategoriaId int, 
	PessoaId int, 
	CentroCustoId int,
	TituloId int)

	insert into @titulos
	exec pr_cria_titulo_virtual @id_empresa, @data_inicial, @data_final

	select *
	from @titulos as T
	where not exists(
		select sum(valor) as valor
		from Liquidacao as L
		where T.TituloId = L.TituloId
		group by L.TituloId
		having sum(valor) >= T.valor)
end

GO

if object_id('pr_titulos_pendentes') > 0
begin
   print '<< CREATE pr_titulos_pendentes >>'
end
GO