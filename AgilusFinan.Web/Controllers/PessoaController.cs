﻿using System.Web.Mvc;
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
        }

        protected override void PreAlteracao(PessoaViewModel viewModel)
        {
            base.PreAlteracao(viewModel);
            ViewBag.ListaBancos = new RepositorioBanco().Listar();
            ViewBag.ListaTiposTelefone = new RepositorioTipoTelefone().Listar();
        }

        protected override void ModelToViewModel(Pessoa model, PessoaViewModel viewModel)
        {
            base.ModelToViewModel(model, viewModel);
            viewModel.ContaBancaria = model.ContaBancaria;
            viewModel.Cpf = model.Cpf;
            viewModel.DataNascimento = model.DataNascimento;
            viewModel.Endereco = model.Endereco;
            viewModel.Nome = model.Nome;
            viewModel.Rg = model.Rg;

            foreach (var t in model.Telefones)
            {
                //viewModel.Telefones.Add(new TelefonePessoaViewModel() {Telefone = t.Telefone, Id = t.Id});
                viewModel.Telefones.Add(new TelefonePessoaViewModel() { Ddd = t.Telefone.Ddd, Numero = t.Telefone.Numero, TipoTelefoneId = t.Telefone.TipoTelefoneId, Id = t.Id });
            }

            foreach (var tp in model.TiposPessoa)
            {
                var tipo = viewModel.TiposPorPessoa.Find(t => t.Id == tp.TipoPessoaId);
                if (tipo != null)
                {
                    tipo.Marcado = true;
                }
            }
        }

        protected override void ViewModelToModel(PessoaViewModel viewModel, Pessoa model)
        {
            base.ViewModelToModel(viewModel, model);
            model.ContaBancaria = viewModel.ContaBancaria;
            model.Id = viewModel.Id;
            model.Cpf = viewModel.Cpf;
            model.DataNascimento = viewModel.DataNascimento;
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
                if (tp.Marcado)
                {
                    model.TiposPessoa.Add(new TipoPessoaPorPessoa() { TipoPessoaId = tp.Id, PessoaId = viewModel.Id });
                }
            }
        }
    }
}