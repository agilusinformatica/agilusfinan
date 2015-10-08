using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public class GeradorFluxoCaixa : IConsulta<FluxoCaixa>
    {
        public List<FluxoCaixa> ChamarProcedimento(Filtro filtro)
        {
            List<FluxoCaixa> Lista = new List<FluxoCaixa>();

            using (Contexto context = new Contexto())
            {
                Lista = context.Database.SqlQuery<FluxoCaixa>("exec pr_fluxo_caixa @empresa, @dataInicial, @dataFinal",
                            new SqlParameter("@empresa", context.EmpresaId),
                            new SqlParameter("@dataInicial", filtro.ValorPorNome("dataInicial")),
                            new SqlParameter("@dataFinal", filtro.ValorPorNome("dataFinal"))).ToList();
            }
            return Lista;
        }

        public Filtro DefineFiltro()
        {
            var filtro = new Filtro();
            filtro.Parametros.Add(new ParametroFiltro() { Nome = "dataInicial", Label = "Data Inicial", Tipo = TipoFiltro.data, Valor = new DateTime(DateTime.Now.Year, 1, 1).Date });
            filtro.Parametros.Add(new ParametroFiltro() { Nome = "dataFinal", Label = "Data Final", Tipo = TipoFiltro.data, Valor = new DateTime(DateTime.Now.Year, 12, 31).Date });

            return filtro;
        }
    }
}
