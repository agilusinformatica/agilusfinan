using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using AgilusFinan.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<string> SegundaViaCnpj(string cpfCnpj)
        {
            var dataAtual = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

            var context = new Contexto();

            string query = String.Format("exec pr_obter_iuguid '{0}', {1}",
                cpfCnpj,
                context.EmpresaId.ToString()
            );
            try
            {
                var retornoPrObterIuguId = context.Database.SqlQuery<RetornoPrIuguId>(query).FirstOrDefault();
                if (retornoPrObterIuguId.dataVencimento >= DateTime.Today)
                {
                    return retornoPrObterIuguId.urlFatura;
                }
                else
                {
                    string faturaJson = await SegundaVia(retornoPrObterIuguId.iuguId, dataAtual);
                    var faturaIugu = new JavaScriptSerializer().Deserialize<FaturaResponse>(faturaJson);
                    return faturaIugu.secure_url;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            };
        }

        [HttpPost]
        public async Task<string> SegundaVia(string iuguId, string dataVencimentoSelecionada)
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
                var address = "https://api.iugu.com/v1/invoices/" + iuguId + "/duplicate?api_token=" + tokenIUGU;

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
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

    public class IuguIdT
    {
        public string IuguId { get; set; }
    }

    public class SegundaVia {
        public string due_date { get; set; }
        public string ignore_due_email { get; set; }
        public string ignore_canceled_email { get; set; }
        public string current_fines_option { get; set; }
    }

    public class RetornoPrIuguId {
        public string iuguId { get; set; }
        public DateTime dataVencimento { get; set; }
        public string urlFatura { get; set; }
    }

}