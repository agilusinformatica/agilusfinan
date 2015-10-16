using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgilusFinan.Web.Bases;
using AgilusFinan.Infra.Services;
using AgilusFinan.Domain.Entities;

namespace AgilusFinan.Web.Controllers
{
    public class ContaController : ControllerPadrao<Conta,RepositorioConta>
    {
        protected override void PreInclusao()
        {
            base.PreInclusao();
            ViewBag.BancoBoletoId = new SelectList(new RepositorioBanco().Listar(),"Id","Nome");
        }
    }
}