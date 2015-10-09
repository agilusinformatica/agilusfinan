using AgilusFinan.Infra.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Controllers
{
    public class SaldoController : Controller
    {
        // GET: Saldo
        //public PartialViewResult Index(DateTime data, int contaId)

        public ActionResult Index(DateTime data, int? contaId)
        {
            var saldo = GeradorSaldo.ChamarProcedimentoSaldo(data, contaId);

            return View(saldo);
        }
    }
}