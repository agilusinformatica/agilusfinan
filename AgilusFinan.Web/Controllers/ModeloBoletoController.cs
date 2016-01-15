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
    public class ModeloBoletoController : ControllerPadrao<ModeloBoleto, RepositorioModeloBoleto>
    {
        protected override void PreInclusao()
        {
            base.PreInclusao();
            ViewBag.ContaId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome");
        }

        protected override void PreAlteracao(ModeloBoleto model)
        {
            base.PreAlteracao(model);
            ViewBag.ContaId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome", model.ContaId);
        }

        [ValidateInput(false)]
        [HttpPost]
        public override ActionResult Create(ModeloBoleto model)
        {
            return base.Create(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public override ActionResult Edit(ModeloBoleto model)
        {
            return base.Edit(model);
        }
    }
}