using AgilusFinan.Domain.Entities;
using AgilusFinan.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Controllers
{
    public class LoteBoletoController : Controller
    {
        // GET: LoteBoleto
        public ActionResult Index()
        {
            return View();
        }

        private void ViewModelToModel(TituloViewModel viewModel, Titulo model)
        {

        }
    }
}