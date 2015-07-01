using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using AgilusFinan.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Controllers
{
    public class RecebimentoController : ControllerViewModelPadrao<Titulo, RepositorioRecebimento, TituloViewModel>
    {
        protected override string FolderViewName()
        {
            return "Titulo";
        }

        protected override void PreInclusao()
        {
            base.PreInclusao();
            ViewBag.ContaId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome");
            ViewBag.CategoriaId = new SelectList(new RepositorioCategoria().Listar().Where(c => c.Direcao == DirecaoCategoria.Recebimento), "Id", "Nome");
            ViewBag.ListaCategorias = Util.CategoriasIdentadas(DirecaoCategoria.Recebimento);
            ViewBag.PessoaId = new SelectList(new RepositorioPessoa().Listar(), "Id", "Nome");
            ViewBag.CentroCustoId = new SelectList(new RepositorioCentroCusto().Listar(), "Id", "Nome");
            GerarLista();
        }

        protected override void PreAlteracao(TituloViewModel viewModel)
        {
            base.PreAlteracao(viewModel);
            ViewBag.ContaId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome", viewModel.ContaId);
            ViewBag.CategoriaId = new SelectList(new RepositorioCategoria().Listar().Where(c => c.Direcao == DirecaoCategoria.Recebimento), "Id", "Nome", viewModel.CategoriaId);
            ViewBag.PessoaId = new SelectList(new RepositorioPessoa().Listar(), "Id", "Nome", viewModel.PessoaId);
            ViewBag.ListaCategorias = Util.CategoriasIdentadas(DirecaoCategoria.Recebimento);
            ViewBag.CentroCustoId = new SelectList(new RepositorioCentroCusto().Listar(), "Id", "Nome", viewModel.CentroCustoId);
            GerarLista();
        }

        private void GerarLista()
        {
            ViewBag.TipoTitulo = "Recebimento";
        }

        protected override void ModelToViewModel(Titulo model, TituloViewModel viewModel)
        {
            base.ModelToViewModel(model, viewModel);
            viewModel.Id = model.Id;
            viewModel.CategoriaId = model.CategoriaId;
            viewModel.CentroCustoId = model.CentroCustoId;
            viewModel.PessoaId = model.PessoaId;
            viewModel.Competencia = model.Competencia;
            viewModel.ContaId = model.ContaId;
            viewModel.DataVencimento = model.DataVencimento;
            viewModel.Descricao = model.Descricao;
            viewModel.Valor = model.Valor;
            viewModel.Observacao = model.Observacao;

            foreach (var l in model.Liquidacoes)
            {
                model.Liquidacoes.Add(new Liquidacao()
                {
                    Id = l.Id,
                    Data = l.Data,
                    Valor = l.Valor,
                    JurosMulta = l.JurosMulta,
                    FormaLiquidacao = l.FormaLiquidacao
                });
            }
        }

        protected override void ViewModelToModel(TituloViewModel viewModel, Titulo model)
        {
            base.ViewModelToModel(viewModel, model);
            model.Id = viewModel.Id;
            model.CategoriaId = viewModel.CategoriaId;
            model.CentroCustoId = viewModel.CentroCustoId;
            model.PessoaId = viewModel.PessoaId;
            model.Competencia = viewModel.Competencia;
            model.ContaId = viewModel.ContaId;
            model.DataVencimento = viewModel.DataVencimento;
            model.Descricao = viewModel.Descricao;
            model.Valor = viewModel.Valor;
            model.Observacao = viewModel.Observacao;

            foreach (var l in viewModel.Liquidacoes)
            {
                model.Liquidacoes.Add(new Liquidacao()
                {
                    Data = l.Data,
                    Valor = l.Valor,
                    JurosMulta = l.JurosMulta,
                    FormaLiquidacao = l.FormaLiquidacao,
                    TituloId = viewModel.Id
                });
            }
        }

    }

}