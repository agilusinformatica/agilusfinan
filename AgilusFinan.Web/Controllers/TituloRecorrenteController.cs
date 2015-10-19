using System.Web.Mvc;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using System.Collections.Generic;
using System.Linq;



namespace AgilusFinan.Web.Controllers
{
    public class TituloRecorrenteController : ControllerPadrao<TituloRecorrente, RepositorioTituloRecorrente>
    {
        protected override void PreInclusao()
        {
            base.PreInclusao();
            ViewBag.ContaId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome", new RepositorioConta().Listar(x => x.Padrao).Any() ? new RepositorioConta().Listar(x => x.Padrao).Single().Id : 0);
            ViewBag.PessoaId = new SelectList(new RepositorioPessoa().Listar(), "Id", "Nome");
            ViewBag.CategoriaId = new SelectList(new RepositorioCategoria().Listar(), "Id", "Nome");
            ViewBag.CentroCustoId = new SelectList(new RepositorioCentroCusto().Listar(), "Id", "Nome");

            ViewBag.ListaCategorias = Util.CategoriasIdentadas(null);

        }

        protected override void PreAlteracao(TituloRecorrente model)
        {
            base.PreAlteracao(model);
            ViewBag.ContaId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome", model.ContaId);
            ViewBag.PessoaId = new SelectList(new RepositorioPessoa().Listar(), "Id", "Nome", model.PessoaId);
            ViewBag.CategoriaId = new SelectList(new RepositorioCategoria().Listar(), "Id", "Nome", model.CategoriaId);
            ViewBag.CentroCustoId = new SelectList(new RepositorioCentroCusto().Listar(), "Id", "Nome", model.CentroCustoId);
            ViewBag.ListaCategorias = Util.CategoriasIdentadas(null);
        }
    }
}