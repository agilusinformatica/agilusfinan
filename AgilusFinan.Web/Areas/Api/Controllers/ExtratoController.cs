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
    public class ExtratoController : ControllerConsultaApiPadrao<GeradorExtrato, Extrato>
    {
        public override Filtro DefineTipoFiltro(NameValueCollection parametros)
        {
            Filtro filtro = new Filtro();

            var dataInicial = parametros.Get("data_inicial");
            var dataFinal = parametros.Get("data_final"); ;
            var ContaId = parametros.Get("ContaId");

            filtro.AdicionaParametro("data_inicial", dataInicial, TipoFiltro.data);
            filtro.AdicionaParametro("data_final", dataInicial, TipoFiltro.data);
            filtro.AdicionaParametro("ContaId", ContaId, TipoFiltro.conta);

            return filtro;
        }
    }
}