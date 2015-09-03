using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgilusFinan.Web.Bases;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;

namespace AgilusFinan.Web.Controllers
{
    public class ConviteController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.PerfilId = new SelectList(new RepositorioPerfil().Listar(), "Id", "Descricao");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            return RedirectToAction("Usuario");
        }
    }
}