using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgilusFinan.Web.Bases;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;

namespace AgilusFinan.Web.Controllers
{
    public class PagamentoController : ControllerPadrao<Pagamento, RepositorioPagamento>
    {
        protected override void PreInclusao()
        {
            base.PreInclusao();
            GerarLista();
        }

        protected override void PreAlteracao()
        {
            base.PreAlteracao();
            GerarLista();
        }

        private void GerarLista()
        {
            ViewBag.ContaId = new SelectList(new RepositorioConta().Listar(), "Id", "nome");
            ViewBag.CategoriaId = new SelectList(new RepositorioCategoria().Listar().Where(c => c.Direcao == DirecaoCategoria.Pagamento), "Id", "nome");
            ViewBag.PessoaId = new SelectList(new RepositorioPessoa().Listar(), "Id", "nome");
            ViewBag.CentroCustoId = new SelectList(new RepositorioCentroCusto().Listar(), "Id", "nome");
        }

    }
}