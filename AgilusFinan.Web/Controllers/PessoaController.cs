using System.Web.Mvc;
using System.Collections.Generic;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using AgilusFinan.Web.ViewModels;

namespace AgilusFinan.Web.Controllers
{
    public class PessoaController : ControllerViewModelPadrao<Pessoa, RepositorioPessoa, PessoaViewModel>
    {
        protected override void PreInclusao()
        {
            base.PreInclusao();
            ViewBag.ListaBancos = new RepositorioBanco().Listar();
            ViewBag.ListaTiposTelefone = new RepositorioTipoTelefone().Listar();
            ViewBag.ListaTiposPessoa = new SelectList(new RepositorioTipoPessoa().Listar(), "Id", "Nome");
            ViewBag.ListaPessoaIndicacao = new SelectList(new RepositorioPessoa().Listar(), "Id", "Nome");
        }

        protected override void PreAlteracao(PessoaViewModel viewModel)
        {
            base.PreAlteracao(viewModel);
            ViewBag.ListaBancos = new RepositorioBanco().Listar();
            ViewBag.ListaTiposTelefone = new RepositorioTipoTelefone().Listar();
            ViewBag.ListaTiposPessoa = new MultiSelectList(new RepositorioTipoPessoa().Listar(), "Id", "Nome", viewModel.TiposPorPessoa.ToArray());
            ViewBag.ListaPessoaIndicacao = new SelectList(new RepositorioPessoa().Listar(), "Id", "Nome", viewModel.PessoaIndicacao);
        }
    }
}