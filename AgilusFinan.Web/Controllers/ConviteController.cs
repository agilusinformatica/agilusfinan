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
            string token = Criptografia.Encriptar(convite.Email + "|" + convite.PerfilId.ToString() + "|" + UsuarioLogado.EmpresaId.ToString());
            var repo = new RepositorioConvite();
            repo.Incluir(convite);
            string remetente = new RepositorioUsuario().BuscarPorId(UsuarioLogado.UsuarioId).Email;
            var Email = new Email(convite.Email, "http://localhost:8197/Login/EfetivarConvite?token=" + token, "Convite", remetente);
            Email.DispararMensagem();

            return RedirectToAction("Index", "Usuario");
        }
    }
}