using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using AgilusFinan.Web.ViewModels;

namespace AgilusFinan.Web.Controllers
{
    public class CategoriaController : ControllerPadrao<Categoria, RepositorioCategoria>
    {


        protected override void PreListagem()
        {
            ViewBag.ListaIdentada = Util.CategoriasIdentadas(null, 0);
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


        public ActionResult BuscaCategorias(DirecaoCategoria direcao, int? categoriaPaiId)
        {
            return PartialView("_ItensCategoria", new ItensCategoria() {Id = categoriaPaiId, Lista = Util.CategoriasIdentadas(direcao)});
        }

       
    }
}