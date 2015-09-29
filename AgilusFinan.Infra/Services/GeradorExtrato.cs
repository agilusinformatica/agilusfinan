using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    class GeradorExtrato
    {
        public static List<Extrato> ChamarProcedimento(int ContaId, DateTime dataInicial, DateTime dataFinal)
        {
            List<Extrato> Lista = new List<Extrato>();

            using (Contexto context = new Contexto())
            {
                Lista = context.Database.SqlQuery<Extrato>("exec pr_extrato @id_empresa, @id_conta, @data_inicial, @data_final",
                            new SqlParameter("@id_empresa", context.EmpresaId),
                            new SqlParameter("@id_conta", ContaId),
                            new SqlParameter("@data_inicial", dataInicial),
                            new SqlParameter("@data_final", dataFinal)).ToList();
            }
            return Lista;
        }
    }
}
