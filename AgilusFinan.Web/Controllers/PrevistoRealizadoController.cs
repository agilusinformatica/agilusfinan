using AgilusFinan.Infra.Services;
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
            return PartialView("~/Views/PrevistoRealizado/_Index.cshtml", GeradorPrevistoRealizado.ChamarProcedimentoPrevistoRealizado(dataInicial, dataFinal));
        }
    }
}