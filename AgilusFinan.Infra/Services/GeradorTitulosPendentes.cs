using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public static class GeradorTitulosPendentes
    {
        public static List<TituloPendente> ChamarProcedimento(DateTime dataInicial, DateTime dataFinal)
        {
            List<TituloPendente> Lista = new List<TituloPendente>();

            using (Contexto context = new Contexto())
            {
                Lista = context.Database.SqlQuery<TituloPendente>("pr_titulos_pendentes @id_empresa, @data_inicial, @data_final",
                            new SqlParameter("@id_empresa", context.EmpresaId),
                            new SqlParameter("@data_inicial", dataInicial),
                            new SqlParameter("@data_final", dataFinal)).ToList();
            }
            return Lista;
        }
    }
}
