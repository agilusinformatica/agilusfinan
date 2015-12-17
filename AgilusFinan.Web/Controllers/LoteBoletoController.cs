using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using AgilusFinan.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void GerarBoletos(string postedData)
        {
            var js = new JavaScriptSerializer();
            var boletos = js.Deserialize<List<LoteBoleto>>(postedData);
            
            //System.IO.StreamWriter = new System.IO.StreamWriter();

            foreach (var boleto in boletos)
            {
                if(boleto.TituloId != null)
                {
                    Util.EnviarBoletoPorEmail((int)boleto.TituloId, Server.MapPath(@"~/App_Data/teste.pdf"), boleto.ModeloBoletoId);
                }
                else
                {
                    Util.EnviarBoletoPorEmail((int)boleto.TituloRecorrenteId, boleto.Valor, boleto.DataVencimento, Server.MapPath(@"~/App_Data/teste.pdf"), boleto.ModeloBoletoId);
                }
            }
        }
 


    }
}