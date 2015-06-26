using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using AgilusFinan.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Controllers
{
    public class RecebimentoController : ControllerViewModelPadrao<Titulo, RepositorioRecebimento, TituloViewModel>
    {
        protected override string FolderViewName()
        {
            return "Titulo";
        }
    }
}