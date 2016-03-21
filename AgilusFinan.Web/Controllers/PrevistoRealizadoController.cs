using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Controllers
{
    public class PrevistoRealizadoController : Controller
    {
        // GET: PrevistoRealizado
        public PartialViewResult Index(DateTime dataInicial, DateTime dataFinal)
        {
            //Parametros do cache
            var parametros = new Dictionary<string, string>();
            parametros.Add("empresaId", UsuarioLogado.EmpresaId.ToString());
            parametros.Add("dataInicial", dataInicial.ToString());
            parametros.Add("dataFinal", dataFinal.ToString());
            var pagina = (PartialViewResult)Cache.Busca("previstorealizado", parametros);

            //se o cache não existir ou foi expirado anteriormente, cria um novo
            if (pagina == null)
            {
                pagina = PartialView("~/Views/PrevistoRealizado/_Index.cshtml", GeradorPrevistoRealizado.ChamarProcedimentoPrevistoRealizado(dataInicial, dataFinal));
                Cache.Insere("previstorealizado", parametros, pagina);
            }

            return pagina;
        }
    }
}