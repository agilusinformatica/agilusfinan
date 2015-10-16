if object_id('fn_feriado') > 0
begin
   drop function fn_feriado
   print '<< DROP fn_feriado >>'
end

GO

create function fn_feriado (@data datetime)
RETURNS bit
/*----------------------------------------------------------------------------------------------------------------------
NOME: fn_feriado
OBJETIVO: Define o saldo na data 
DATA: 14/10/2015
TESTES: select dbo.fn_feriado('2015-12-25')
----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------*/
begin
	declare @feriado bit

	if exists(select 1 from FeriadoRegional where data = @data)	or 
	   exists(select 1 from FeriadoGeral where data = @data)
	   set @feriado = 1
	else 
	   set @feriado = 0

	return @feriado 
end
GO

if object_id('fn_feriado') > 0
begin
   print '<< CREATE fn_feriado >>'
end
GO
