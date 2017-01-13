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
    public class ParametroController : ControllerPadrao<Parametro, RepositorioParametro>
    {
        [ValidateInput(false)]
        [HttpPost]
        public override ActionResult Edit(Parametro model)
        {
            base.Edit(model);
            return RedirectToAction("Index", "Home");
        }
    }
}