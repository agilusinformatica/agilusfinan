using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using AgilusFinan.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AgilusFinan.Web.Controllers
{
    public class FaturaGeradaController : ControllerPadrao<FaturaGerada, RepositorioFaturaGerada>
    {
        public ActionResult IndexData(string dataInicial, string dataFinal)
        {
            Session.Add("dataInicial", dataInicial);
            Session.Add("dataFinal", dataFinal);

            var parametros = new Dictionary<string, string>();
            parametros.Add("empresaId", UsuarioLogado.EmpresaId.ToString());
            parametros.Add("dataInicial", dataInicial.ToString());
            parametros.Add("dataFinal", dataFinal.ToString());
            var pagina = (PartialViewResult)Cache.Busca("fatura", parametros);

            if (pagina == null)
            {
                DateTime dI, dF;
                if (String.IsNullOrEmpty(dataInicial) && String.IsNullOrEmpty(dataFinal))
                {
                    dI = Util.PrimeiroDiaMes(DateTime.Today);
                    dF = Util.UltimoDiaMes(DateTime.Today);
                }
                else
                {
                    dI = DateTime.ParseExact(dataInicial, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    dF = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                var faturas = repo.Listar(t => t.DataVencimento >= dI && t.DataVencimento <= dF);
                
                pagina = PartialView("~/Views/FaturaGerada/IndexData.cshtml", faturas);
                Cache.Insere("fatura", parametros, pagina);
            }
            return pagina;
        }

        [HttpGet]
        public ActionResult SegundaVia(string iuguId)
        {
            var folder = FolderViewName();
            ViewBag.IuguId = iuguId;
            return PartialView("~/Views/FaturaGerada/_SegundaViaModal.cshtml", DateTime.Today);
        }

        [HttpPost]
        public string SegundaVia(string iuguId, string dataVencimentoSelecionada)
        {
            var segundaVia = new SegundaVia();

            segundaVia.current_fines_option = "true";
            segundaVia.due_date = dataVencimentoSelecionada;
            segundaVia.ignore_due_email = "false";
            segundaVia.ignore_canceled_email = "true";

            var js = new JavaScriptSerializer();
            var segundaViaJSON = js.Serialize(segundaVia);

            var tokenIUGU = new RepositorioParametro().Listar().FirstOrDefault().TokenIUGU;
            var response = "";

            using (WebClient client = new WebClient())
            {

                client.Encoding = System.Text.Encoding.UTF8;

                client.Headers.Add("Content-Type", "application/json");
                client.Headers.Add("Authorization", "Basic " + tokenIUGU);
                var address = "https://api.iugu.com/v1/invoices/" + iuguId + "/duplicate";

                response = client.UploadString(address, "POST", segundaViaJSON);
            }

            var faturaResponse = js.Deserialize<FaturaResponse>(response);
            if (faturaResponse != null)
            {
                var repoFaturaGerada = new RepositorioFaturaGerada();
                var faturaAntiga = repo.Listar().Where(fg => fg.IuguId == iuguId).FirstOrDefault();
                faturaAntiga.IuguId = faturaResponse.id;
                faturaAntiga.UrlFatura = faturaResponse.secure_url;

                repoFaturaGerada.Alterar(faturaAntiga);
            }
            return response;
        }


    }

    public class SegundaVia {
        public string due_date { get; set; }
        public string ignore_due_email { get; set; }
        public string ignore_canceled_email { get; set; }
        public string current_fines_option { get; set; }
    }
}