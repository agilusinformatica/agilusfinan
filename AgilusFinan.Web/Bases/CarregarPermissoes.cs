using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Bases
{
    public class CarregarPermissoes : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Gravar esses acessos em algum local para que em futuros acessos ele não precise validar no servidor,
            //por exemplo, no cookie de autenticação.
            UsuarioLogado.Acessos = AcessosUsuario.Listar(UsuarioLogado.UsuarioId);
        }
    }
}