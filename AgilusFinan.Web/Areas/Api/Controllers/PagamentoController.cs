using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Areas.Api.Bases;
using AgilusFinan.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Areas.Api.Controllers
{
    public class PagamentoController : ControllerViewModelApiPadrao<Titulo, RepositorioPagamento, TituloViewModel>
    {
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
            viewModel.TituloRecorrenteId = model.TituloRecorrenteId;
            viewModel.TipoTitulo = "Pagamento";

            foreach (var l in model.Liquidacoes)
            {
                viewModel.Liquidacoes.Add(new LiquidacaoViewModel()
                {
                    Id = l.Id,
                    Data = l.Data,
                    Valor = l.Valor,
                    JurosMulta = l.JurosMulta,
                    FormaLiquidacao = l.FormaLiquidacao,
                    Desconto = l.Desconto
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
            model.Valor = viewModel.Valor > 0 ? (decimal)viewModel.Valor : 0;
            model.Observacao = viewModel.Observacao;
            model.TituloRecorrenteId = viewModel.TituloRecorrenteId;

            foreach (var l in viewModel.Liquidacoes)
            {
                model.Liquidacoes.Add(new Liquidacao()
                {
                    Data = l.Data,
                    Valor = l.Valor,
                    JurosMulta = l.JurosMulta,
                    FormaLiquidacao = l.FormaLiquidacao,
                    TituloId = viewModel.Id,
                    Desconto = l.Desconto
                });
            }
        }
    }
}