if object_id('fn_gerador_vencimentos') > 0
begin
   drop function fn_gerador_vencimentos
   print '<< DROP fn_gerador_vencimentos >>'
end

GO

create function fn_gerador_vencimentos(@id_titulo_recorrente int,@data_inicial datetime, @data_final datetime, @dia_vencimento tinyint, @qtde_parcelas int, @data_primeiro_vencimento datetime, @tipo_recorrencia tinyint)
/*----------------------------------------------------------------------------------------------------------------------
NOME: fn_gerador_vencimentos
OBJETIVO: Gerar os vencimentos de acordo com recorr�ncia de cada t�tulo
DATA: 22/07/2015
TESTES: 
select * from fn_gerador_vencimentos(1,'2015-07-01', '2015-07-31', 2, null, '2015-07-15', 0)
select * from fn_gerador_vencimentos(1,'2015-07-01', '2015-07-31', 2, null, '2015-07-01', 1)
select * from fn_gerador_vencimentos(1,'2015-07-01', '2015-07-31', 2, null, '2015-07-15', 2)
select * from fn_gerador_vencimentos(1,'2015-01-01', '2015-12-31', 2, null, '2015-01-01', 3)
select * from fn_gerador_vencimentos(1,'2015-01-01', '2015-12-31', 2, null, '2015-01-01', 4)
select * from fn_gerador_vencimentos(1,'2015-01-01', '2015-12-31', 2, null, '2015-01-01', 5)
select * from fn_gerador_vencimentos(1,'2015-01-01', '2016-12-31', 2, null, '2015-01-01', 6)
----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------*/
RETURNS @vencimentos table(data_vencimento datetime) 
with encryption
AS
BEGIN 

	declare @cont int = 0,
			@data_final_analise datetime = dateadd(week, +1, @data_final)
   declare 
      @data_base datetime = @data_primeiro_vencimento

	WHILE @data_base <= @data_final_analise
	BEGIN
      set @data_base = 
         case @tipo_recorrencia 
            when 0 then dateadd(week, @cont, @data_primeiro_vencimento)
            when 1 then dateadd(week, @cont*2, @data_primeiro_vencimento)
            when 2 then dateadd(MONTH, @cont, @data_primeiro_vencimento)
            when 3 then dateadd(MONTH, @cont*2, @data_primeiro_vencimento)
            when 4 then dateadd(MONTH, @cont*3, @data_primeiro_vencimento)
            when 5 then dateadd(MONTH, @cont*6, @data_primeiro_vencimento)
            when 6 then dateadd(year, @cont, @data_primeiro_vencimento)
         end
      
      if @dia_vencimento is not null and @tipo_recorrencia >= 2
			set @data_base = @data_base-day(@data_base)+@dia_vencimento
      if @dia_vencimento is not null and @tipo_recorrencia in (0,1)
         while datepart(dw, @data_base) != @dia_vencimento
			   set @data_base = @data_base + 1
			
		WHILE /*dbo.fn_feriado(@data_base) = 1 or */ DATEPART(DW,@data_base) in (7, 1)
		BEGIN
			set @data_base = @data_base-1
		END
		
      if (@data_base >= @data_inicial and @data_base < @data_final+1 and @data_base >= @data_primeiro_vencimento) 
			insert into @vencimentos(data_vencimento)
			select @data_base
			where not exists (
							   select 1 
							   from Titulo 
							   where TituloRecorrenteId = @id_titulo_recorrente
							   and DataVencimento = @data_base)
		set @cont = @cont + 1

      if @cont > @qtde_parcelas-1
         return
	END
   return
END

GO

if object_id('fn_gerador_vencimentos') > 0
begin
   print '<< CREATE fn_gerador_vencimentos >>'
end
GO