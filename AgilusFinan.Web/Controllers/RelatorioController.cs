using AgilusFinan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Controllers
{
    public class RelatorioController : Controller
    {
        // GET: Extrato
        public ActionResult Index(string nomeRelatorio)
        {
            var filtro = new Filtro();
            if (nomeRelatorio == "Extrato")
            {
                filtro.Parametros.Add(new ParametroFiltro() { Nome = "@data_inicial", Label = "Data Inicial", Tipo = TipoFiltro.data, Valor = DateTime.Today });
                filtro.Parametros.Add(new ParametroFiltro() { Nome = "@data_final", Label = "Data Final", Tipo = TipoFiltro.data, Valor = DateTime.Today });
            }
            return View(filtro);
        }

        [HttpPost]
        public ActionResult Consulta(string jsonFiltros)
        {
            return View();
        }


    }
}