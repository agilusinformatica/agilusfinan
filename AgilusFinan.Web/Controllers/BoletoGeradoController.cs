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
    public class BoletoGeradoController : ControllerPadrao<BoletoGerado, RepositorioBoletoGerado>
    {
        public PartialViewResult ExibirBoleto(int boletoGeradoId)
        {
            String boletogerado = String.Empty;
            BoletoGerado b = new RepositorioBoletoGerado().BuscarPorId(boletoGeradoId);
            if(b.TituloId != null){
                boletogerado = Util.GerarBoletoBancario((int)b.TituloId, (int)b.ModeloBoletoId).MontaHtmlEmbedded();
            }
            else{
                boletogerado = Util.GerarBoletoBancario((int)b.TituloRecorrenteId, (decimal)b.TituloRecorrente.Valor, (DateTime)b.DataVencimento, (int)b.ModeloBoletoId).MontaHtmlEmbedded();
            }

            boletogerado = boletogerado.Replace("background-color: #ffffff;","");

            ViewBag.BoletoGerado = boletogerado;
            return PartialView("~/Views/BoletoGerado/_Boleto.cshtml");

        }
    }
}