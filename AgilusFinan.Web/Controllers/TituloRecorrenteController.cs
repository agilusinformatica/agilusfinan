using System.Web.Mvc;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;

namespace AgilusFinan.Web.Controllers
{
    public class TituloRecorrenteController : ControllerPadrao<TituloRecorrente, RepositorioTituloRecorrente>
    {
        protected override void PreInclusao()
        {
            base.PreInclusao();
            ViewBag.PessoaId = new SelectList(new RepositorioPessoa().Listar(), "Id", "Nome");
            ViewBag.CategoriaId = new SelectList(new RepositorioCategoria().Listar(), "Id", "Nome");
            ViewBag.CentroCustoId = new SelectList(new RepositorioCentroCusto().Listar(), "Id", "Nome");
            ViewBag.ListaCategorias = Util.CategoriasIdentadas(null);

        }

        protected override void PreAlteracao()
        {
            base.PreAlteracao();
            base.PreInclusao();
            ViewBag.PessoaId = new SelectList(new RepositorioPessoa().Listar(), "Id", "Nome");
            ViewBag.CategoriaId = new SelectList(new RepositorioCategoria().Listar(), "Id", "Nome");
            ViewBag.CentroCustoId = new SelectList(new RepositorioCentroCusto().Listar(), "Id", "Nome");
            ViewBag.ListaCategorias = Util.CategoriasIdentadas(null);
        }
    }
}