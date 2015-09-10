if object_id('pr_Acessar') > 0
begin
   drop procedure pr_listar_acessos
   print '<< DROP pr_listar_acessos >>'
end

GO

create procedure pr_listar_acessos(@UsuarioId int) 
as
Begin
	set nocount on
	
	select f.Id as Id, f.descricao as Descricao, f.endereco as Endereco, f.FuncaoSuperiorId as FuncaoSuperiorId
	from usuario as u
	inner join perfil as p on p.id = u.PerfilId and p.EmpresaId = u.EmpresaId
	inner join acesso as a on a.PerfilId = p.Id
	inner join funcao as f on f.Id = a.FuncaoId
	where u.id = @UsuarioId
	and u.ativo = 1
end

GO

if object_id('pr_listar_acessos') > 0
begin
   print '<< CREATE pr_listar_acessos >>'
end

GO

--TESTE
--exec pr_listar_acessos 2
--