using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;

namespace AgilusFinan.Web.Controllers
{
    public class CategoriaController : ControllerPadrao<Categoria, RepositorioCategoria>
    {
        
        Dictionary<int, string> lista;
        private List<Categoria> itens;

        protected override void PreInclusao()
        {
            
        }
        
        private void AdicionaItem(Categoria c, int nivel)
        {
            string identador = System.Net.WebUtility.HtmlDecode("&nbsp;");
            lista.Add(c.Id, Repete(identador, nivel*3) + c.Nome);
            var filhas = itens.Where(f => f.CategoriaPaiId == c.Id && f.Id != f.CategoriaPaiId);
            foreach (var item in filhas)
            {
                AdicionaItem(item, ++nivel);
                --nivel;
            }
        }

        private string Repete(string texto, int qtde)
        {
            string retorno = "";
            for (int i = 0; i < qtde; i++)
            {
                retorno += texto;
            }
            return retorno;
        }

        public ActionResult BuscaCategorias(DirecaoCategoria direcao)
        {
            itens = repo.Listar(c => c.Direcao == direcao);
            lista = new Dictionary<int, string>();
            var root = itens.Where(c => c.CategoriaPaiId == null);
            foreach (var item in root)
            {
                AdicionaItem(item, 0);

            }
            return PartialView("_ItensCategoria", lista);
        }
    }

}