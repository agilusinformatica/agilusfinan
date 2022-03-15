if object_id('pr_obter_iuguid') > 0
begin
   drop procedure pr_obter_iuguid
   print '<< DROP pr_obter_iuguid >>'
end

GO

/*----------------------------------------------------------------------------------------------------------------------
NOME: pr_obter_iuguid
OBJETIVO: Pegar o iuguId, datavencimento e url da fatura da ultima fatura do cliente pelo cpf ou cnpj
DATA: 02/03/2022
----------------------------------------------------------------------------------------------------------------------
exec pr_obter_iuguid '04331349000167', 2
----------------------------------------------------------------------------------------------------------------------*/
create procedure pr_obter_iuguid (@cpfCnpj varchar(30), @id_empresa int)    
as  
begin  

	declare @iuguId varchar(100),   
			@tituloId int,   
			@tituloRecorrenteId int,   
			@dataVencimento datetime,
			@urlFatura varchar(100)

	select top 1 @iuguId = fg.IuguId,
				 @TituloRecorrenteId = TituloRecorrenteId,
				 @dataVencimento = dataVencimento,
				 @urlFatura = UrlFatura
	from FaturaGerada fg  
	join TituloRecorrente tr on tr.Id = fg.TituloRecorrenteId  
	join Pessoa p on p.Id = tr.PessoaId  
	where p.Cpf = @cpfCnpj
	and fg.EmpresaId = @id_empresa
	order by fg.DataVencimento desc  

	if @iuguId is null  
	begin  
		raiserror('Fatura não encontrada', 16, 1, 0)  
		return  
	end  

	select @tituloId = Id  
	from Titulo t  
	where TituloRecorrenteId = @TituloRecorrenteId  
	and DataVencimento = @dataVencimento 
 

	if @tituloId is not null  
	begin
		if exists (select * from liquidacao where TituloId = @tituloId)  
		begin  
			raiserror('Fatura já está paga', 16, 1, 0)  
			return  
		end  
	end  
	
	select @iuguId iuguId, @dataVencimento dataVencimento, @urlFatura urlFatura

end
GO

if object_id('pr_obter_iuguid') > 0
begin
   print '<< CREATE pr_obter_iuguid >>'
end
GO