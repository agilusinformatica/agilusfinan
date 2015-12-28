using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using Newtonsoft.Json;
using System.Globalization;

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

        public PartialViewResult VinculoTitulos(string tituloAConciliar, string dataInicial, string dataFinal)
        {
            var titulo = JsonConvert.DeserializeObject<ConciliacaoExtrato>(tituloAConciliar);

            DirecaoCategoria direcao = titulo.TipoLancamento == TipoLancamento.Credito ? DirecaoCategoria.Recebimento : DirecaoCategoria.Pagamento;
            
            var dI = DateTime.ParseExact(dataInicial, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var dF = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var titulosPendentes = GeradorTitulosPendentes.ChamarProcedimento(dI, dF, null).Where(t => t.Direcao == direcao).ToList();

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