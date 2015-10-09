using AgilusFinan.Infra.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public static class GeradorSaldo
    {
        public static Decimal ChamarProcedimentoSaldo(DateTime data, int? contaId)
        {

            using (Contexto context = new Contexto())
            {
                if (contaId == null)
                {
                    return context.Database.SqlQuery<Decimal>("select dbo.fn_saldo(null, @data, @empresaId)",
                        //new SqlParameter("@contaId", contaId),
                        new SqlParameter("@data", data),
                        new SqlParameter("@empresaId", context.EmpresaId)
                    ).FirstOrDefault<Decimal>();
                }
                else
                {
                    return context.Database.SqlQuery<Decimal>("select dbo.fn_saldo(@contaId, @data, @empresaId)",
                        new SqlParameter("@contaId", contaId),
                        new SqlParameter("@data", data),
                        new SqlParameter("@empresaId", context.EmpresaId)
                    ).FirstOrDefault<Decimal>();
                }

            }
        }

    }
}
