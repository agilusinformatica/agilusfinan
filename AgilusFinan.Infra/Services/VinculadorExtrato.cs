using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public static class VinculadorExtrato
    {
        public static void VincularTitulos(DataTable titulosSemVinculo, DataTable titulosNaoCriados)
        {
            using (var db = new Contexto())
            {
                var EmpresaId = new SqlParameter("@EmpresaId", db.EmpresaId);
                var parTSemVinculo = new SqlParameter("@titulos_sem_vinculo", SqlDbType.Structured);
                parTSemVinculo.TypeName = "dbo.TitulosSemVinculo";
                parTSemVinculo.Value = titulosSemVinculo;
                var parTNaoCriados = new SqlParameter("@titulos_nao_criados", SqlDbType.Structured);
                parTNaoCriados.TypeName = "dbo.TitulosNaoCriados";
                parTNaoCriados.Value = titulosNaoCriados;

                db.Database.ExecuteSqlCommand("exec pr_conciliar_titulos @titulos_sem_vinculo, @titulos_nao_criados, @EmpresaId", parTSemVinculo, parTNaoCriados, EmpresaId);
            }

        }
    }
}

