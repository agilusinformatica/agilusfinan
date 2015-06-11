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

        protected override void PreListagem()
        {
            ViewBag.ListaIdentada = CategoriasIdentadas(null);
            var l = CategoriasIdentadas(null);
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

        [HttpGet]
        public ActionResult CreateFilha(DirecaoCategoria direcao, int categoriaPaiId)
        {
            Categoria c = new Categoria();
            c.Direcao = direcao;
            c.CategoriaPaiId = categoriaPaiId;
            PreInclusao();
            ViewBag.Operacao = "IncluindoFilha";
            return View(c);
        }

        [HttpPost]
        public ActionResult CreateFilha(Categoria categoria)
        {
            return base.Create(categoria);
            
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

        public ActionResult BuscaCategorias(DirecaoCategoria direcao, int? categoriaPaiId)
        {
            ViewBag.CategoriaPaiId = categoriaPaiId;
            return PartialView("_ItensCategoria", CategoriasIdentadas(direcao));
        }

        public Dictionary<int, string> CategoriasIdentadas(DirecaoCategoria? direcao)
        {
            if (direcao != null) 
            { 
                itens = repo.Listar(c => c.Direcao == direcao);
            }
            else
            {
                itens = repo.Listar().ToList();
            }
            
            lista = new Dictionary<int, string>();
            var root = itens.Where(c => c.CategoriaPaiId == null);
            foreach (var item in root)
            {
                AdicionaItem(item, 0);
            }

            return lista;
        }
    }
}