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
                filterContext.RouteData.Values["action"] = "Index";
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
                confirmacaoCaminho = encontrar(UsuarioLogado.Acessos, caminho);
            }

            if (String.IsNullOrEmpty(confirmacaoCaminho) || (caminho != confirmacaoCaminho))
            {
                throw new Exception("Você não tem privilégios suficientes para acessar este recurso. Para solicitar este acesso fale com o administrador do sistema.");
            }
        }

        private string encontrar(List<Funcao> lista, string endereco)
        {
            string caminho;

            try
            {
                // vamos pesquisar o valor na lista usando seu método Find()
                // Aqui o primeiro endereço que passar no critério de busca será retornado
                var encontrado = lista.Find(delegate(Funcao valor)
                {
                    return valor.Endereco.Contains(endereco);
                });
                caminho = encontrado.Endereco;
            }
            catch(Exception)
            {
                caminho = String.Empty;
            }
            return caminho;
        }
    }
}