using AgilusFinan.Infra.Services;
using AgilusFinan.Web.ViewModels;
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
        public ActionResult Index(DateTime data, int? contaId)
        {
            var saldos = new List<SaldoViewModel>();
            foreach (var conta in new RepositorioConta().Listar())
            {
                saldos.Add(new SaldoViewModel() {Conta = conta, Saldo = GeradorSaldo.ChamarProcedimentoSaldo(data, conta.Id)});
            }
            return View(saldos);
        }
    }
}