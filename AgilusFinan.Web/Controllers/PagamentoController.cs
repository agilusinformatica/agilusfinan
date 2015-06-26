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
    public class PagamentoController : ControllerPadrao<Titulo, RepositorioPagamento>
    {
        protected override string FolderViewName()
        {
            return "Titulo";
        }
    }
}