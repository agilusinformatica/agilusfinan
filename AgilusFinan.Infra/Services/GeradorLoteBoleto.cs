using AgilusFinan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public class GeradorLoteBoleto : IConsulta<LoteBoleto>
    {
        public List<LoteBoleto> ChamarProcedimento(Filtro filtro)
        {
            List<TituloPendente> titulos = GeradorTitulosPendentes.ChamarProcedimento(filtro.ValorPorNome("data_inicial"), filtro.ValorPorNome("data_final"));

            var loteboletos = new List<LoteBoleto>();

            foreach (var titulo in titulos)
	        {
		        loteboletos.Add(
                    new LoteBoleto { 
                        Boleto = true, 
                        DataVencimento = titulo.DataVencimento,
                        Email = true,
                        PessoaId = titulo.PessoaId,
                        Valor = titulo.Valor
                    });
	        }

            return loteboletos;
        }

        public Filtro DefineFiltro()
        {
            var filtro = new Filtro();
            filtro.Parametros.Add(new ParametroFiltro() { Nome = "data_inicial", Label = "Data Inicial", Tipo = TipoFiltro.data, Valor = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) /*DateTime.Now.AddDays(-DateTime.Now.Day).AddDays(1)*/ });
            filtro.Parametros.Add(new ParametroFiltro() { Nome = "data_final", Label = "Data Final", Tipo = TipoFiltro.data, Valor = DateTime.Now.AddDays(-DateTime.Now.Day).AddDays(1).Date.AddMonths(1).AddDays(-1).Date });
            return filtro;
        }
    }
}
