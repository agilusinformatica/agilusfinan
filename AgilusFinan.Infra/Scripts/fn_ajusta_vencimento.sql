if object_id('fn_ajusta_vencimento') > 0
begin
   drop function fn_ajusta_vencimento
   print '<< DROP fn_ajusta_vencimento >>'
end

GO

create function fn_ajusta_vencimento (@data_base datetime, @direcao_vencimento tinyint)
RETURNS datetime
/*----------------------------------------------------------------------------------------------------------------------
NOME: fn_ajusta_vencimento
OBJETIVO: Se a data for inválida (não útil, feriado) troca para uma data útil
DATA: 11/02/2016
TESTES: select dbo.fn_feriado('2016-02-09')
----------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------*/
begin

	WHILE dbo.fn_feriado(@data_base) = 1 or DATEPART(DW,@data_base) in (7, 1)
	BEGIN
		if @direcao_vencimento = 0 -- antecipar
			set @data_base = @data_base-1
		if @direcao_vencimento = 1 -- postergar
			set @data_base = @data_base+1
	END

	return @data_base
end
GO

if object_id('fn_ajusta_vencimento') > 0
begin
   print '<< CREATE fn_ajusta_vencimento >>'
end
GO
