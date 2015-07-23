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
    public class GeradorTitulosPendentes
    {
        public List<TituloPendente> ChamarProcedimento(int idEmpresa, DateTime dataInicial, DateTime dataFinal)
        {
            List<TituloPendente> Lista = new List<TituloPendente>();

            using (Contexto context = new Contexto())
            {
                Lista = context.Database.SqlQuery<TituloPendente>("pr_cria_titulo_virtual @id_empresa, @data_inicial_analise, @data_final_analise",
                new SqlParameter("@id_empresa", idEmpresa),
                new SqlParameter("@data_inicial_analise", dataInicial),
                new SqlParameter("@data_final_analise", dataFinal)).ToList();
            }
            return Lista;
        }
    }
}
