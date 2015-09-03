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
    public class TituloPendenteController : Controller
    {
        // GET: TitulosPendentes
        public PartialViewResult Index(DateTime dataInicial, DateTime dataFinal)
        {
            return PartialView("~/Views/TituloPendente/_Index.cshtml", GeradorTitulosPendentes.ChamarProcedimento(dataInicial, dataFinal));
        }

        [HttpGet]
        public ActionResult Liquidar(DateTime dataVencimento, int tituloRecorrenteId)
        {
            ViewBag.TipoTitulo = "TituloPendente";
            ViewBag.ContaId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome", new RepositorioTituloRecorrente().BuscarPorId(tituloRecorrenteId).ContaId);
            RepositorioTituloRecorrente repo = new RepositorioTituloRecorrente();
            TituloRecorrente tituloR = repo.BuscarPorId(tituloRecorrenteId);
            TituloViewModel tituloVm =
                new TituloViewModel()
                {
                    Id = tituloRecorrenteId,
                    CategoriaId = tituloR.CategoriaId,
                    Descricao = tituloR.Nome,
                    CentroCustoId = tituloR.CentroCustoId,
                    DataVencimento = dataVencimento,
                    PessoaId = tituloR.PessoaId,
                    Valor = tituloR.Valor
                };
            return View("~/Views/Titulo/Liquidar.cshtml", tituloVm);
        }

        [HttpPost]
        public ActionResult Liquidar(string postedData)
        {
            RepositorioPadrao<Titulo> repo = new RepositorioPadrao<Titulo>();
            TituloViewModel viewModel = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<TituloViewModel>(postedData);

            if (ModelState.IsValid)
            {
                var novoTitulo = new Titulo();
                ViewModelToModel(viewModel, novoTitulo);
                repo.Incluir(novoTitulo);
            }
            return RedirectToAction("Index");
        }

        private void ViewModelToModel(TituloViewModel viewModel, Titulo model)
        {
            model.Id = 0;
            model.CategoriaId = viewModel.CategoriaId;
            model.CentroCustoId = viewModel.CentroCustoId;
            model.ContaId = viewModel.ContaId; //Valor não estava vindo, fiz um update no js da view
            model.DataVencimento = viewModel.DataVencimento;
            model.Descricao = viewModel.Descricao;
            model.TituloRecorrenteId = viewModel.Id;
            model.PessoaId = viewModel.PessoaId;
            model.Valor = viewModel.Valor != null ? (decimal)viewModel.Valor : viewModel.Liquidacoes.Sum(m => m.Valor);
            
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