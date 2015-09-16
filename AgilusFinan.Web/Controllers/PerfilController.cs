using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;
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
    public class PerfilController : ControllerViewModelPadrao<Perfil, RepositorioPerfil, PerfilViewModel>
    {
        protected override void PreInclusao()
        {
            base.PreInclusao();
            ViewBag.ListaFuncoes = Util.FuncoesIdentadas();
        }
        protected override void PreAlteracao(PerfilViewModel viewModel)
        {
            base.PreAlteracao(viewModel);
            ViewBag.ListaFuncoes = Util.FuncoesIdentadas();
        }

        protected override void ModelToViewModel(Perfil model, PerfilViewModel viewModel)
        {
            base.ModelToViewModel(model, viewModel);

            viewModel.Id = model.Id;
            viewModel.Descricao = model.Descricao;

            var funcoes = new Contexto().Funcoes.ToList();
            foreach (var funcao in funcoes )
            {
                viewModel.Acessos.Add(new ItemAcesso() 
                { 
                    FuncaoId = funcao.Id, 
                    Descricao = funcao.Descricao, 
                    FuncaoSuperiorId = funcao.FuncaoSuperiorId, 
                    Selecionado = model.Acessos.Exists(f => f.FuncaoId == funcao.Id)
                });
            }
        }

        protected override void ViewModelToModel(PerfilViewModel viewModel, Perfil model)
        {
            base.ViewModelToModel(viewModel, model);

            model.Id = viewModel.Id;
            model.Descricao = viewModel.Descricao;

            foreach (var acesso in viewModel.Acessos)
            {
                if (acesso.Selecionado)
                {
                    model.Acessos.Add(new Acesso()
                    {
                        FuncaoId = acesso.FuncaoId,
                        PerfilId = viewModel.Id
                    });
                }
            }
        }
    }
}