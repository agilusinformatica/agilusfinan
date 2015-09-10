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
        List<Funcao> AcessoValidado;

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //called after a controller action is executed 
            ValidarAcessos();
        }

        public void ValidarAcessos()
        {
            AcessoValidado = ListarAcessosUsuario.ChamarFuncaoAcesso(UsuarioLogado.UsuarioId);
            UsuarioLogado.Acessos = AcessoValidado;
            //Gravar esses acessos em algum local para que em futuros acessos ele não precise validar no servidor,
            //por exemplo, no cookie de autenticação.
        }
    }
}