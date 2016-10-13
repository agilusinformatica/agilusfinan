using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Utils;
using AgilusFinan.Infra.Context;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using BoletoNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Controllers
{
    public class AtualizacaoBoletoController : Controller
    {


        private BoletoBancario Gerador(string token)
        {
            Contexto db = new Contexto();
            //Decriptar o boleto
            var idBoletoGerado = Convert.ToInt32(Criptografia.Decriptar(token));

            BoletoGerado boletoGerado = db.BoletosGerado.Find(idBoletoGerado);
            boletoGerado.TituloRecorrente = db.TitulosRecorrentes.Find(boletoGerado.TituloRecorrenteId);
            boletoGerado.Titulo = db.Titulos.Find(boletoGerado.TituloId);

            //gerar o boleto 
            if (boletoGerado.TituloId != null)
            {
                return Util.GerarBoletoBancario((int)boletoGerado.TituloId, boletoGerado.ModeloBoletoId);
            }
            else
            {
                return Util.GerarBoletoBancario((int)boletoGerado.TituloRecorrenteId, (decimal)boletoGerado.TituloRecorrente.Valor, (DateTime)boletoGerado.DataVencimento, boletoGerado.ModeloBoletoId);
            }
        }

        // GET: AtualizacaoBoleto
        [AllowAnonymous]
        public ViewResult Index(string tokenBoleto)
        {

            ViewBag.BoletoBancario = Gerador(tokenBoleto).MontaHtmlEmbedded();
            ViewBag.Token = tokenBoleto;
            return View();
            
        }
        [AllowAnonymous]
        public FileResult Baixar(string tokenBoleto)
        {
            var ms = new MemoryStream();
            try
            {
                ms = Util.StringToPdf(Gerador(tokenBoleto).MontaHtmlEmbedded(false, true));
            }
            catch (Exception)
            {
                throw new Exception("A API não está respondendo. Tente novamente mais tarde. Se o erro persistir favor entrar em contato com o nosso suporte pelo número (11) 3266-3124");
            }

            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=Boleto Agilus.pdf");

            //retornar o boleto com a data de vencimento atualizada
            return new FileStreamResult(ms, "application/pdf");
        }
    }
}