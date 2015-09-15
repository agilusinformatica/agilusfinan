using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgilusFinan.Web.Bases;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Domain.Utils;

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
        public ActionResult Create(Convite convite)
        {
            string host = Request.Url.Scheme+@"://"+Request.Url.Host+":"+Request.Url.Port.ToString();
            Util.EnviarConvite(convite, UsuarioLogado.EmpresaId, new RepositorioUsuario().BuscarPorId(UsuarioLogado.UsuarioId).Email, host);
            var repo = new RepositorioConvite();
            repo.Incluir(convite);
            return RedirectToAction("Index", "Usuario");
        }
    }
}