using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public class GeradorExtrato : IConsulta<Extrato>
    {
        public List<Extrato> ChamarProcedimento(Filtro filtro)
        {
            List<Extrato> Lista = new List<Extrato>();

            using (Contexto context = new Contexto())
            {
                Lista = context.Database.SqlQuery<Extrato>("exec pr_extrato @id_empresa, @id_conta, @data_inicial, @data_final",
                            new SqlParameter("@id_empresa", context.EmpresaId),
                            new SqlParameter("@id_conta", filtro.ValorPorNome("ContaId")),
                            new SqlParameter("@data_inicial", filtro.ValorPorNome("data_inicial")),
                            new SqlParameter("@data_final", filtro.ValorPorNome("data_final"))).ToList();
            }
            return Lista;
        }

        public Filtro DefineFiltro()
        {
            var filtro = new Filtro();
            filtro.Parametros.Add(new ParametroFiltro() { Nome = "ContaId", Label = "Conta", Tipo = TipoFiltro.texto });
            filtro.Parametros.Add(new ParametroFiltro() { Nome = "data_inicial", Label = "Data Inicial", Tipo = TipoFiltro.data, Valor = DateTime.Today });
            filtro.Parametros.Add(new ParametroFiltro() { Nome = "data_final", Label = "Data Final", Tipo = TipoFiltro.data, Valor = DateTime.Today });
            return filtro;
        }

    }
}
