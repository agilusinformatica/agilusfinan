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
    public class PagamentoController : ControllerViewModelPadrao<Titulo, RepositorioPagamento, TituloViewModel>
    {
        protected override string FolderViewName()
        {
            return "Titulo";
        }

        protected override void PreInclusao()
        {
            base.PreInclusao();
            ViewBag.Liquidacoes = new RepositorioLiquidacao().Listar();
        }

        protected override void PreAlteracao()
        {
            base.PreAlteracao();
            ViewBag.ListaLiquidacoes = new RepositorioLiquidacao().Listar();
        }

        protected override void ModelToViewModel(Titulo model, TituloViewModel viewModel)
        {
            base.ModelToViewModel(model, viewModel);
            viewModel.Categoria = model.Categoria;
            viewModel.CategoriaId = model.CategoriaId;
            viewModel.CentroCusto = model.CentroCusto;
            viewModel.CentroCustoId = model.CentroCustoId;
            viewModel.Competencia = model.Competencia;
            viewModel.Conta = model.Conta;
            viewModel.Id = model.ContaId;
            viewModel.DataVencimento = model.DataVencimento;
            viewModel.Descricao = model.Descricao;

            foreach (var l in model.Liquidacoes)
            {
                model.Liquidacoes.Add(new Liquidacao()
                {
                    Id = l.Id,
                    Data = l.Data,
                    Valor = l.Valor,
                    JurosMulta = l.JurosMulta,
                    FormaLiquidacao = l.FormaLiquidacao,
                    TituloId = l.TituloId
                });
            }
        }

        protected override void ViewModelToModel(TituloViewModel viewModel, Titulo model)
        {
            base.ViewModelToModel(viewModel, model);
            model.Categoria = viewModel.Categoria;
            model.CategoriaId = viewModel.CategoriaId;
            model.CentroCusto = viewModel.CentroCusto;
            model.CentroCustoId = viewModel.CentroCustoId;
            model.Competencia = viewModel.Competencia;
            model.Conta = viewModel.Conta;
            model.ContaId = viewModel.Id;
            model.DataVencimento = viewModel.DataVencimento;
            model.Descricao = viewModel.Descricao;

            foreach (var l in viewModel.Liquidacoes)
            {
                model.Liquidacoes.Add(new Liquidacao()
                {
                    Id = l.Id,
                    Data = l.Data,
                    Valor = l.Valor,
                    JurosMulta = l.JurosMulta,
                    FormaLiquidacao = l.FormaLiquidacao,
                    TituloId = l.TituloId
                });
            }
        }
    }
}