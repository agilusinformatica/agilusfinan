if object_id('pr_resumo_titulo_categoria') > 0
begin
   drop procedure pr_resumo_titulo_categoria
   print '<< DROP pr_resumo_titulo_categoria >>'
end

GO

create procedure pr_resumo_titulo_categoria(@id_empresa int, @data_inicial smalldatetime, @data_final smalldatetime)
/*----------------------------------------------------------------------------------------------------------------------
NOME: pr_cria_titulo_virtual
OBJETIVO: Consulta de valores dos títulos por categoria
DATA: 07/08/2015
EXEMPLO: exec pr_resumo_titulo_categoria 1,'20150101','20151231'
----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------*/
as 
Begin
	 select Id as CategoriaId, Nome, CategoriaPaiId, Cor, dbo.fn_soma_categoria(id, @data_inicial, @data_final) as Valor
	 from Categoria 
	 where EmpresaId = @id_empresa
	 order by Direcao, CategoriaPaiId
end
GO

if object_id('pr_resumo_titulo_categoria') > 0
begin
   print '<< CREATE pr_resumo_titulo_categoria >>'
end
GO