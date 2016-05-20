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
    public class PessoaController : ControllerViewModelApiPadrao<Pessoa, RepositorioPessoa, PessoaViewModel>
    {
        protected override void ModelToViewModel(Pessoa model, PessoaViewModel viewModel)
        {
            base.ModelToViewModel(model, viewModel);
            viewModel.Id = model.Id;
            viewModel.ContaBancaria = model.ContaBancaria;
            viewModel.Cpf = model.Cpf;
            viewModel.DataNascimento = model.DataNascimento;
            viewModel.EmailContato = model.EmailContato;
            viewModel.EmailFinanceiro = model.EmailFinanceiro;
            viewModel.Endereco = model.Endereco;
            viewModel.Nome = model.Nome;
            viewModel.Rg = model.Rg;

            foreach (var t in model.Telefones)
            {
                viewModel.Telefones.Add(new TelefonePessoaViewModel() { Ddd = t.Telefone.Ddd, Numero = t.Telefone.Numero, TipoTelefoneId = t.Telefone.TipoTelefoneId, Id = t.Id });
            }

            foreach (var tp in model.TiposPessoa)
            {
                viewModel.TiposPorPessoa.Add(tp.TipoPessoaId);
            }
        }
        protected override void ViewModelToModel(PessoaViewModel viewModel, Pessoa model)
        {
            base.ViewModelToModel(viewModel, model);
            model.ContaBancaria = viewModel.ContaBancaria;
            model.Id = viewModel.Id;
            model.Cpf = viewModel.Cpf;
            model.DataNascimento = viewModel.DataNascimento;
            model.EmailFinanceiro = viewModel.EmailFinanceiro;
            model.EmailContato = viewModel.EmailContato;
            model.Endereco = viewModel.Endereco;
            model.Nome = viewModel.Nome;
            model.Rg = viewModel.Rg;

            foreach (var t in viewModel.Telefones)
            {
                model.Telefones.Add(new TelefonePessoa()
                {
                    Id = t.Id,
                    PessoaId = viewModel.Id,
                    Telefone = new Telefone()
                    {
                        Ddd = t.Ddd,
                        Numero = t.Numero,
                        TipoTelefoneId = t.TipoTelefoneId
                    }
                });
            }

            foreach (var tp in viewModel.TiposPorPessoa)
            {
                model.TiposPessoa.Add(new TipoPessoaPorPessoa() { TipoPessoaId = tp, PessoaId = viewModel.Id });
            }
        }
    }
}