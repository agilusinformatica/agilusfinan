using AgilusFinan.Domain.Entities;
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
            
            //Remover esta linha quando tiver solução para logoff.
            if (UsuarioLogado.EmpresaId == 0)
            {
                filterContext.RouteData.Values["controller"] = "Login";
                filterContext.RouteData.Values["action"] = "Logoff";
                                
                return;
            }
            string caminho = filterContext.RouteData.Values["controller"].ToString();
            string confirmacaoCaminho = String.Empty;

            if (filterContext.RouteData.Values["action"] != null)
            {
                caminho = caminho + "/" + filterContext.RouteData.Values["action"].ToString();
            }

            if (caminho != null)
            {
                if (!UsuarioLogado.Acessos.Exists(f => f.Endereco == caminho))
                    throw new Exception("Você não tem privilégios suficientes para acessar este recurso. Para solicitar este acesso fale com o administrador do sistema.");
            }
        }
    }
}