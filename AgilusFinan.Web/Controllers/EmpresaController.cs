using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Utils;
using AgilusFinan.Infra.Context;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Controllers
{
    public class EmpresaController : Controller
    {
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View(new Empresa());
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Create(Empresa empresa, HttpPostedFileBase file)
        {
            Contexto db = new Contexto();
            empresa.Ativo = true;
            db.Empresas.Add(empresa);
            db.SaveChanges();

            Perfil perfil = new Perfil()
            {
                Descricao = "Administrador",
                EmpresaId = empresa.Id
            };

            db.Perfis.Add(perfil);
            db.SaveChanges();


            // criação do convite
            Convite convite = new Convite()
            {
                Email = empresa.EmailContato,
                EmpresaId = empresa.Id,
                Expirado = false,
                PerfilId = perfil.Id
            };
            db.Convites.Add(convite);


            // inclusão das permissões no perfil
            foreach (var funcao in db.Funcoes)
            {
                Acesso acesso = new Acesso() {PerfilId = perfil.Id, FuncaoId = funcao.Id};
                db.Acessos.Add(acesso);
            }

            if (file != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    empresa.Logotipo = ms.ToArray();
                }
            }

            db.SaveChanges();

            Util.EnviarConvite(convite, empresa.Id, "henrique@agilus.com.br");

            return RedirectToAction("Index", "Login");
        }



    }
}