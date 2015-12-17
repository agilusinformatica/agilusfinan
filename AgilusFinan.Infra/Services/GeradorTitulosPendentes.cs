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
        public static List<TituloPendente> ChamarProcedimento(DateTime dataInicial, DateTime dataFinal, int? tipoPessoa)
        {
            List<TituloPendente> Lista = new List<TituloPendente>();

            using (Contexto context = new Contexto())
            {
                string query = String.Format("exec pr_titulos_pendentes {0}, '{1}', '{2}', {3}",
                    context.EmpresaId.ToString(),
                    dataInicial.ToString("yyyy-MM-dd"),
                    dataFinal.ToString("yyyy-MM-dd"),
                    tipoPessoa == null ? "null" : tipoPessoa.ToString()
                );

                Lista = context.Database.SqlQuery<TituloPendente>(query).ToList();

                /*Lista = context.Database.SqlQuery<TituloPendente>("exec pr_titulos_pendentes @id_empresa, @data_inicial, @data_final, @tipo_pessoa",
                            new SqlParameter("@id_empresa", context.EmpresaId),
                            new SqlParameter("@data_inicial", dataInicial),
                            new SqlParameter("@data_final", dataFinal),
                            new SqlParameter("@tipo_pessoa", tipoPessoa)).ToList();*/
            }
            return Lista;
        }
    }
}
