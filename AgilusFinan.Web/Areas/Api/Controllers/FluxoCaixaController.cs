using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Areas.Api.Bases;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Areas.Api.Controllers
{
    public class FluxoCaixaController : ControllerConsultaApiPadrao<GeradorFluxoCaixa,FluxoCaixa>
    {
        public override Filtro DefineTipoFiltro(NameValueCollection parametros)
        {
            Filtro filtro = new Filtro();

            var dataInicial = parametros.Get("dataInicial");
            var dataFinal = parametros.Get("dataFinal"); ;
            var ContaId = parametros.Get("periodicidade");

            filtro.AdicionaParametro("dataInicial", dataInicial, TipoFiltro.data);
            filtro.AdicionaParametro("dataFinal", dataFinal, TipoFiltro.data);
            filtro.AdicionaParametro("periodicidade", ContaId, TipoFiltro.periodicidade);

            return filtro;
        }
    }
}