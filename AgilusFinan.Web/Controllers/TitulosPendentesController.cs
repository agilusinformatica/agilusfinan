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
    public class TitulosPendentesController : Controller
    {
        // GET: TitulosPendentes
        public ActionResult Index(int idEmpresa)
        {
            GeradorTitulosPendentes titulosPendentes = new GeradorTitulosPendentes();
            DateTime dataInicio = DateTime.Now.AddMonths(-2), DataFim = DateTime.Now;
            List<TituloPendente> Titulos = new List<TituloPendente>();

            Titulos = titulosPendentes.ChamarProcedimento(idEmpresa, dataInicio, DataFim);

            return View(Titulos);
        }
    }
}