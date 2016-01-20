using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using AgilusFinan.Web.ViewModels;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
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
        public string GerarBoleto(string postedData)
        {
            var js = new JavaScriptSerializer();
            var boleto = js.Deserialize<LoteBoleto>(postedData);
            Util.EnviarBoletoPorEmail(boleto, Server.MapPath(@"~/App_Data/teste.pdf"));

            return "ok";
        }

        [HttpPost]
        public string BaixarBoletos(string postedData)
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
                    var nomeArquivo = Server.MapPath(@"~/App_Data/" + boleto.NomePessoa + seq.ToString() + ".pdf");
                    seq++;
                    if (boleto.TituloId != null)
                    {
                        Util.SalvarBoleto((int)boleto.TituloId, nomeArquivo, boleto.ModeloBoletoId);
                    }
                    else
                    {
                        Util.SalvarBoleto((int)boleto.TituloRecorrenteId, boleto.Valor, boleto.DataVencimento, nomeArquivo, boleto.ModeloBoletoId);
                    }
                    zip.AddFile(nomeArquivo, String.Empty);
                }
                zip.Save(Server.MapPath(@"~/Content/" + random + ".Zip"));
            }

            return Util.EnderecoHost() + "/Content/" + random + ".Zip";
        }
    }
}