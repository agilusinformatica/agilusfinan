using System.Collections.Generic;
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

        protected override void PreInclusao()
        {
            lista = new Dictionary<int, string>();
            var root = repo.Listar(c => c.Id == c.CategoriaPaiId);
            foreach (var item in root)
            {
                AdicionaItem(item, 0);

            }
            ViewBag.CategoriasPai = lista;
        }
        
        private void AdicionaItem(Categoria c, int nivel)
        {
            string identador = System.Net.WebUtility.HtmlDecode("&nbsp;");
            lista.Add(c.Id, Repete(identador, nivel*2) + c.Nome);
            var filhas = repo.Listar(f => f.CategoriaPaiId == c.Id && f.Id != f.CategoriaPaiId);
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
    }

}