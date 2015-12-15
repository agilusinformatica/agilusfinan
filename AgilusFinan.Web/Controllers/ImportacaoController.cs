using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
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
            var extrato = Parser.InterpretarOfx(file.InputStream);
            var dataInicial = extrato.Select(d => d.DataLancamento).Min();
            var dataFinal = extrato.Select(d => d.DataLancamento).Max();
            var titulosPendentes = GeradorTitulosPendentes.ChamarProcedimento(dataInicial, dataFinal);

            //foreach (var item in extrato)
            //{
            //    var titulos = titulosPendentes.FindAll(t => t.Valor == item.Valor);
            //    vinculoLista.Add(new ItemVinculoBanco { ItemExtrato = item, TitulosPendentes = titulos});    

            //}

            return View("ConciliacaoExtrato", new ItemVinculoBanco{Extrato = extrato, TitulosPendentes = titulosPendentes});
        }


        public JsonResult ConcilarTitulos(string titulosAConciliar)
        {
            //magic happens

            //then..
            return new JsonResult{ Data = JsonConvert.SerializeObject(titulosAConciliar)};
        }
    }

    public class ItemVinculoBanco
    {
        public List<ConciliacaoExtrato> Extrato { get; set; }
        public List<TituloPendente> TitulosPendentes { get; set; }
    }
}