using System.Web.Mvc;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;

namespace AgilusFinan.Web.Controllers
{
    public class CategoriaController : ControllerPadrao<Categoria, RepositorioCategoria>
    {
        Contexto db = new Contexto();

        protected override void PreInclusao()
        {
            ViewBag.IdCategoria = new SelectList(db.Categorias, "Id", "Nome");
        }
    }
}