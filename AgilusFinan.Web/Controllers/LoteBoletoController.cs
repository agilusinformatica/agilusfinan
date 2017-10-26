using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using AgilusFinan.Web.ViewModels;
using BoletoNet;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AgilusFinan.Web.Controllers
{
    public class LoteBoletoController : ControllerConsultaPadrao<GeradorLoteBoleto, LoteBoleto>
    {

        public override string FolderViewName()
        {
            return "LoteBoleto";
        }

        [HttpPost]
        public string GerarFatura(string postedData)
        {
            var js = new JavaScriptSerializer();
            var boleto = js.Deserialize<LoteBoleto>(postedData);
            var fatura = new FaturaViewModel();
            var tokenIUGU = new RepositorioParametro().Listar().FirstOrDefault().TokenIUGU;
            var response = "";
            string faturaJSON = "";
            try
            {
                //Util.EnviarBoletoPorEmail(boleto, "boleto.pdf");
                if (boleto.TituloId != null)
                    fatura = Util.GerarFatura(boleto);
                else
                    fatura = Util.GerarFatura((int)boleto.TituloRecorrenteId, boleto.ModeloBoletoId, boleto.DataVencimento, boleto.Valor);
                

                faturaJSON = js.Serialize(fatura);

                using (WebClient client = new WebClient()) {

                    client.Encoding = System.Text.Encoding.UTF8;

                    client.Headers.Add("Content-Type", "application/json");
                    client.Headers.Add("Authorization", "Basic " + tokenIUGU);

                    response = client.UploadString("https://api.iugu.com/v1/invoices", "POST", faturaJSON);
                }

                var faturaResponse = js.Deserialize<FaturaResponse>(response);
                if (faturaResponse != null)
                {
                    var faturaGerada = new FaturaGerada();
                    faturaGerada.DataVencimento = DateTime.Parse(faturaResponse.due_date);
                    faturaGerada.IuguId = faturaResponse.id;
                    faturaGerada.TituloId = boleto.TituloId;
                    faturaGerada.TituloRecorrenteId = boleto.TituloRecorrenteId;
                    faturaGerada.UrlFatura = faturaResponse.secure_url;

                    var repoFaturaGerada = new RepositorioFaturaGerada();
                    repoFaturaGerada.Incluir(faturaGerada);
                }

                return "Enviado";
            }
            catch (WebException e)
            {
                return StreamToString(e.Response.GetResponseStream()) + "      " + faturaJSON;

            }
            
        }

        private string StreamToString(Stream str)
        {
            StreamReader reader = new StreamReader(str);
            return reader.ReadToEnd();
        }

        [HttpPost]
        public FileResult BaixarBoletos(string postedData)
        {
            var js = new JavaScriptSerializer();
            var boletos = js.Deserialize<List<LoteBoleto>>(postedData);
            var random = Path.GetRandomFileName().Substring(0, 5);

            using (ZipFile zip = new ZipFile())
            {
                Random rnd = new Random();
                int seq = 1;
                foreach (var boleto in boletos)
                {
                    var nomeArquivo = boleto.NomePessoa + seq.ToString() + ".html";
                    seq++;
                    Stream html;
                    if (boleto.TituloId != null)
                    {
                        html = Util.SalvarBoleto((int)boleto.TituloId, boleto.ModeloBoletoId);
                    }
                    else
                    {
                        html = Util.SalvarBoleto((int)boleto.TituloRecorrenteId, boleto.Valor, boleto.DataVencimento, boleto.ModeloBoletoId);
                    }
                    html.Flush();
                    html.Seek(0, SeekOrigin.Begin);

                    zip.AddEntry(nomeArquivo, html);
                }
                MemoryStream ms = new MemoryStream();
                zip.Save(ms);
                ms.Seek(0, SeekOrigin.Begin);
                ms.Flush();

                return File(ms, "application/zip", "Boletos.zip");

            }
        }

        [HttpPost]
        public FileResult GerarRemessa(string postedData)
        {
            var js = new JavaScriptSerializer();
            var loteboletos = js.Deserialize<List<LoteBoleto>>(postedData);
            var random = Path.GetRandomFileName().Substring(0, 5);
            Boleto boletoGerado = new Boleto();

            MemoryStream strm = new MemoryStream();
            ArquivoRemessa arquivoRemessa = new ArquivoRemessa(TipoArquivo.CNAB400);
            Boletos boletos = new Boletos();

            var convenio = new RepositorioModeloBoleto().BuscarPorId(loteboletos.First().ModeloBoletoId).Convenio;

            foreach (var boleto in loteboletos)
            {
                if (boleto.TituloId != null)
                {
                    boletoGerado = Util.GerarBoleto((int)boleto.TituloId, boleto.ModeloBoletoId);
                }
                else
                {
                    boletoGerado = Util.GerarBoleto((int)boleto.TituloRecorrenteId, boleto.Valor, boleto.DataVencimento, boleto.ModeloBoletoId);
                }
                boletos.Add(boletoGerado);
            }

            arquivoRemessa.GerarArquivoRemessa(convenio, boletoGerado.Banco, boletoGerado.Cedente, boletos, strm, 1);

            return File(strm.ToArray(), "text/plain", "teste.txt");
        }
    }
}