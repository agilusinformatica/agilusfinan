using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;
using AgilusFinan.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Bases
{
    public class Permissao : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            string caminho = filterContext.RouteData.Values["controller"].ToString();
            string confirmacaoCaminho = String.Empty;

            if (filterContext.RouteData.Values["action"] != null)
            {
                caminho = caminho + "/" + filterContext.RouteData.Values["action"].ToString();
            }

            if (caminho != null)
            {
                using (var db = new Contexto())
                {
                    var a = (from acesso in db.Acessos
                            join funcao in db.Funcoes on acesso.FuncaoId equals funcao.Id
                            where funcao.Endereco == caminho && acesso.PerfilId == UsuarioLogado.PerfilId
                            select acesso).Count();

                    if (a == 0)
                        throw new Exception("Você não tem privilégios suficientes para acessar este recurso. Para solicitar este acesso fale com o administrador do sistema.");
                }
            }
        }
    }
}