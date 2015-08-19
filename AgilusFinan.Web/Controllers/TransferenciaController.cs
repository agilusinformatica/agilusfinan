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
    public class TransferenciaController : ControllerPadrao<Transferencia, RepositorioTransferencia>
    {
        protected override void PreInclusao()
        {
            base.PreInclusao();
            ViewBag.ContaOrigemId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome");
            ViewBag.ContaDestinoId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome");
        }

    }
}