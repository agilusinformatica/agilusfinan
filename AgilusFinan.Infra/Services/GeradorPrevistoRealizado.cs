using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;
using System.Data.SqlClient;

namespace AgilusFinan.Infra.Services
{
    public static class GeradorPrevistoRealizado
    {
        public static List<PrevistoRealizado> ChamarProcedimentoPrevistoRealizado(DateTime dataInicial, DateTime dataFinal)
        {
            List<PrevistoRealizado> Lista = new List<PrevistoRealizado>();

            using (Contexto context = new Contexto())
            {
                Lista = context.Database.SqlQuery<PrevistoRealizado>("pr_previsto_realizado @id_empresa, @data_inicial, @data_final",
                new SqlParameter("@id_empresa", context.EmpresaId),
                new SqlParameter("@data_inicial", dataInicial),
                new SqlParameter("@data_final", dataFinal)).ToList();
            }
            return Lista;
        }
    }
}
