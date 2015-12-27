using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using AgilusFinan.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Controllers
{
    public class TransferenciaController : ControllerPadrao<Transferencia, RepositorioTransferencia>
    {
        protected override void PreInclusao()
        {
            base.PreInclusao();
            ViewBag.ContaOrigemId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome");
            ViewBag.ContaDestinoId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome");
        }

        protected override void PreAlteracao(Transferencia model)
        {
            base.PreAlteracao(model);
            ViewBag.ContaOrigemId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome");
            ViewBag.ContaDestinoId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome");
        }

        protected override IEnumerable<Transferencia> Dados()
        {
            return new List<Transferencia>();
        }

        public ActionResult IndexData(string dataInicial, string dataFinal)
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
            return PartialView("~/Views/Transferencia/IndexData.cshtml", repo.Listar(t => t.Data >= dI && t.Data <= dF));
        }

    }
}