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
            DateTime dataInicial = Convert.ToDateTime(filtro.ValorPorNome("data_inicial"));
            DateTime dataFinal = Convert.ToDateTime(filtro.ValorPorNome("data_final"));
            String tipo = filtro.ValorPorNome("tipo_pessoa");
            int? tipoPessoa = null;
            if (!String.IsNullOrWhiteSpace(tipo))
            {
                tipoPessoa = Convert.ToInt32(tipo);
            }

            ModeloBoleto modeloBoleto = new RepositorioModeloBoleto().BuscarPorId(Convert.ToInt32(filtro.ValorPorNome("modelo_boleto")));

            List<TituloPendente> titulos = GeradorTitulosPendentes.ChamarProcedimento(dataInicial, dataFinal, tipoPessoa).Where(i => i.Direcao == DirecaoCategoria.Recebimento).Where(m => m.ContaId == modeloBoleto.ContaId).ToList();

            var loteboletos = new List<LoteBoleto>();

            foreach (var titulo in titulos)
	        {
		        loteboletos.Add(
                    new LoteBoleto { 
                        TituloId = titulo.TituloId,
                        TituloRecorrenteId = titulo.TituloRecorrenteId,
                        Boleto = true, 
                        DataVencimento = titulo.DataVencimento,
                        Email = true,
                        NomePessoa = titulo.NomePessoa,
                        Valor = titulo.Valor.GetValueOrDefault(0.0M),
                        ModeloBoletoId = modeloBoleto.Id
                    });
	        }
            return loteboletos;
        }

        public Filtro DefineFiltro()
        {
            var filtro = new Filtro();
            filtro.Parametros.Add(new ParametroFiltro() { Nome = "tipo_pessoa", Label = "Tipo de Pessoa", Tipo = TipoFiltro.tipopessoa });
            filtro.Parametros.Add(new ParametroFiltro() { Nome = "data_inicial", Label = "Data Inicial", Tipo = TipoFiltro.data, Valor = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) });
            filtro.Parametros.Add(new ParametroFiltro() { Nome = "data_final", Label = "Data Final", Tipo = TipoFiltro.data, Valor = DateTime.Now.AddDays(-DateTime.Now.Day).AddDays(1).Date.AddMonths(1).AddDays(-1).Date });
            filtro.Parametros.Add(new ParametroFiltro() { Nome = "modelo_boleto", Label = "Modelo de Boleto", Tipo = TipoFiltro.modeloboleto });

            return filtro;
        }
    }
}
