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
            ViewBag.BancoId = new SelectList(new RepositorioBanco().Listar(),"Id","Nome");
        }

        protected override void PreAlteracao(Conta model)
        {
            base.PreAlteracao(model);
            ViewBag.BancoId = new SelectList(new RepositorioBanco().Listar(), "Id", "Nome", model.BancoId);
        }
    }
}