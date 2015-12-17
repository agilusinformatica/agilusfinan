using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using Newtonsoft.Json;

namespace AgilusFinan.Web.Controllers
{
    public class ImportacaoController : Controller
    {
        // GET: ConciliacaoExtrato
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConciliacaoExtrato(HttpPostedFileBase file)
        {
            return View("ConciliacaoExtrato", Parser.InterpretarOfx(file.InputStream));
        }

        public PartialViewResult VinculoTitulos(string tituloAConciliar)
        {
            var titulo = JsonConvert.DeserializeObject<ConciliacaoExtrato>(tituloAConciliar);
            var dataInicial =  titulo.DataLancamento.AddDays(-10);
            var dataFinal = titulo.DataLancamento.AddDays(10) ;
            var titulosPendentes = GeradorTitulosPendentes.ChamarProcedimento(dataInicial, dataFinal, null);

            return PartialView("_VinculoTitulos", titulosPendentes);
        }


        public JsonResult ConcilarTitulos(string titulosAConciliar)
        {
            //magic happens

            //then..
            return new JsonResult{ Data = JsonConvert.SerializeObject(titulosAConciliar)};
        }
    }

}