using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;

namespace AgilusFinan.Web.Controllers
{
    public class ResumoTituloCategoriaController : Controller
    {
        // GET: ResumoTituloCategoria
        public ActionResult Index()
        {
            DateTime dataInicial = DateTime.Now.AddDays(-DateTime.Now.Day).AddDays(1).Date, 
                     dataFinal = dataInicial.AddMonths(1).AddDays(-1).Date;

            return View(GeradorResumoTituloCategoria.ChamarProcedimentoResumoCategoria(dataInicial, dataFinal));
        }
    }
}