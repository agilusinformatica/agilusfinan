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
    public class UsuarioController : ControllerPadrao<Usuario, RepositorioUsuario>
    {
        protected override void PreInclusao()
        {
            base.PreInclusao();
            ViewBag.PerfilId = new SelectList(new RepositorioPerfil().Listar(), "Id", "Descricao");
        }

        protected override void PreAlteracao(Usuario model)
        {
            base.PreAlteracao(model);
            ViewBag.PerfilId = new SelectList(new RepositorioPerfil().Listar(), "Id", "Descricao");
        }
    }
}