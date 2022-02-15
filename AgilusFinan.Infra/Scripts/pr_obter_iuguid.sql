if object_id('pr_obter_iuguid') > 0
begin
   drop procedure pr_obter_iuguid
   print '<< DROP pr_obter_iuguid >>'
end

GO

CREATE procedure pr_obter_iuguid (@id_empresa int, @cpf varchar(30))    
as  
begin  
  
 declare @iuguId varchar(100),   
   @tituloId int,   
   @tituloRecorrenteId int,   
   @dataVencimento datetime  
   
 select top 1 @iuguId = fg.IuguId, @TituloRecorrenteId = TituloRecorrenteId, @dataVencimento = dataVencimento  
 from FaturaGerada fg  
 join TituloRecorrente tr on tr.Id = fg.TituloRecorrenteId  
 join Pessoa p on p.Id = tr.PessoaId  
 where p.Cpf = @cpf  
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
  
 select @iuguId  
  
end

GO

if object_id('pr_obter_iuguid') > 0
begin
   print '<< CREATE pr_obter_iuguid >>'
end
GO