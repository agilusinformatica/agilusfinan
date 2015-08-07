using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;
using System.Data.SqlClient;

namespace AgilusFinan.Infra.Services
{
    public class GeradorResumoTituloCategoria
    {

        public static List<ResumoTituloCategoria> ChamarProcedimentoResumoCategoria(DateTime dataInicial, DateTime dataFinal)
        {
            List<ResumoTituloCategoria> Lista = new List<ResumoTituloCategoria>();

            using (Contexto context = new Contexto())
            {
                Lista = context.Database.SqlQuery<ResumoTituloCategoria>("pr_resumo_titulo_categoria @id_empresa, @data_inicial, @data_final",
                new SqlParameter("@id_empresa", context.EmpresaId),
                new SqlParameter("@data_inicial", dataInicial),
                new SqlParameter("@data_final", dataFinal)).ToList();
            }
            return Lista;
        }
    }
}
