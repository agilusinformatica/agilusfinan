using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgilusFinan.Web.Bases;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using System.Collections;

namespace AgilusFinan.Web.Controllers
{
    public class RecebimentoController : ControllerPadrao<Recebimento, RepositorioRecebimento>
    {
        protected override void PreInclusao()
        {
            base.PreInclusao();
            GerarListaCampos();
        }

        private void GerarListaCampos()
        {
            ViewBag.CategoriaId = new SelectList(new RepositorioCategoria().Listar(c => c.Direcao == DirecaoCategoria.Recebimento).ToList(), "Id", "Nome");
            ViewBag.PessoaId = new SelectList(new RepositorioPessoa().Listar(), "Id", "Nome");
            ViewBag.CentroCustoId = new SelectList(new RepositorioCentroCusto().Listar(), "Id", "Nome");
            ViewBag.ContaId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome");
        }

        protected override void PreAlteracao()
        {
            base.PreAlteracao();
            GerarListaCampos();

        }

    }
}