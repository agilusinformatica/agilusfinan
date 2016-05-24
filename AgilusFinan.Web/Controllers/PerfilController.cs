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
            ViewBag.ListaFuncoes = Util.FuncoesIdentadas(0);
            ViewBag.Funcoes = new Contexto().Funcoes.ToList();
        }
        protected override void PreAlteracao(PerfilViewModel viewModel)
        {
            base.PreAlteracao(viewModel);
            ViewBag.ListaFuncoes = Util.FuncoesIdentadas(0);
            ViewBag.Funcoes = new Contexto().Funcoes.ToList();

        }
    }
}