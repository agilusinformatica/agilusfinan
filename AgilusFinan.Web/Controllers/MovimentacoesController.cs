using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Controllers
{
    public class MovimentacoesController : Controller
    {
        // GET: Movimentacoes
        public ActionResult Recebimento()
        {
            return RedirectToAction("Index", "Recebimento");
        }

        public ActionResult Pagamento()
        {
            return RedirectToAction("Index", "Pagamento");
        }
    }
}