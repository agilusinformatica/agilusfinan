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
    public class FluxoCaixaController : ControllerConsultaPadrao<GeradorFluxoCaixa, FluxoCaixa>
    {
        public override string FolderViewName()
        {
            return "FluxoCaixa";
        }
    }
}