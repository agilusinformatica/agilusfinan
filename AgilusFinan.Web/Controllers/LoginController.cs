using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Utils;
using AgilusFinan.Infra.Context;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AgilusFinan.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(string usuario, string senha)
        {
            Login.ValidaLogin(usuario, senha);
            FormsAuthentication.SetAuthCookie(usuario, false);
            Sessao.EmpresaId = UsuarioLogado.EmpresaId;
            Sessao.PerfilId = UsuarioLogado.PerfilId;
            Sessao.UsuarioId = UsuarioLogado.UsuarioId;

            string returnUrl = Request.Form["returnUrl"];
            if (this.Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");

            //return View();
        }

     
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult EfetivarConvite(string token)
        {
            string decriptToken = Criptografia.Decriptar(token);
            string[] array = decriptToken.Split('|');
            string email = array[0];
            Session["perfilIdConvite"] = Convert.ToInt16(array[1]);
            Session["empresaIdConvite"] = Convert.ToInt16(array[2]);

            return View(new Usuario() { Email = email });
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult EfetivarConvite(Usuario user)
        {
            var db = new Contexto();
            user.Ativo = true;
            user.PerfilId =  (short)Session["perfilIdConvite"];
            user.EmpresaId = (short)Session["empresaIdConvite"];
            Session.Remove("perfilIdConvite");
            Session.Remove("empresaIdConvite");
            db.Usuarios.Add(user);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

    }
}