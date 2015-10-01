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
    public class ExtratoController : ControllerConsultaPadrao<GeradorExtrato, Extrato>
    {
        public override string FolderViewName()
        {
            return "Extrato";
        }

        public override void PreFiltro()
        {
            base.PreFiltro();
            ViewBag.Contas = new RepositorioConta().Listar();
        }


    }
}