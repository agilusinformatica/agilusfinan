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
            Session.Add("dataInicial", dataInicial.ToString("dd/MM/yyyy"));
            Session.Add("dataFinal", dataFinal.ToString("dd/MM/yyyy"));

            var parametros = new Dictionary<string, string>();
            parametros.Add("empresaId", UsuarioLogado.EmpresaId.ToString());
            parametros.Add("dataInicial", dataInicial.ToString());
            parametros.Add("dataFinal", dataFinal.ToString());
            var pagina = (PartialViewResult)Cache.Busca("titulopendente", parametros);
            
            if (pagina == null)
	        {
                pagina = PartialView("~/Views/TituloPendente/_Index.cshtml", GeradorTitulosPendentes.ChamarProcedimento(dataInicial, dataFinal, null));
                Cache.Insere("titulopendente", parametros, pagina);
	        }
            return pagina;
        }

        [HttpGet]
        [Permissao]
        public ActionResult Liquidar(DateTime dataVencimento, int tituloRecorrenteId)
        {
            RepositorioTituloRecorrente repo = new RepositorioTituloRecorrente();
            TituloRecorrente tituloR = repo.BuscarPorId(tituloRecorrenteId);
            ViewBag.TipoTitulo = "TituloPendente";
            ViewBag.ControllerRetorno = Util.NomeControllerAnterior();
            ViewBag.ContaId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome", tituloR.ContaId);
            TituloViewModel tituloVm =
                new TituloViewModel()
                {
                    TituloRecorrenteId = tituloRecorrenteId,
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
        [Permissao]
        public void Liquidar(string postedData)
        {
            RepositorioPadrao<Titulo> repo = new RepositorioPadrao<Titulo>();
            TituloViewModel viewModel = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<TituloViewModel>(postedData);
            if (viewModel.TituloRecorrenteId != null && (viewModel.Valor  == null ||viewModel.Valor == 0))
            {
                viewModel.Valor = viewModel.Liquidacoes.Sum(l => l.Valor);
            }

            if (ModelState.IsValid)
            {
                var novoTitulo = viewModel.ToModel();
                repo.Incluir(novoTitulo);
                TempData["Alerta"] = new Alerta() { Mensagem = "Título liquidado com sucesso", Tipo = "success" };
            }
        }
        [Permissao]
        public ActionResult LiquidarDiretamente(DateTime dataVencimento, int tituloRecorrenteId)
        {
            var repo = new RepositorioTituloRecorrente();
            var tituloR = repo.BuscarPorId(tituloRecorrenteId);

            if(tituloR.Valor == 0 || tituloR.Valor == null)
            {
                throw new Exception("Para liquidar um título ele deve ter um valor.");
            }
            
            //Criação do título referente ao título recorrente
            var titulo = new Titulo()
            {
                CategoriaId = tituloR.CategoriaId,
                CentroCustoId = tituloR.CentroCustoId,
                ContaId = tituloR.ContaId,
                EmpresaId = tituloR.EmpresaId,
                PessoaId = tituloR.PessoaId,
                TituloRecorrenteId = tituloR.Id,
                Valor = (decimal)tituloR.Valor,
                DataVencimento = dataVencimento,
                Descricao = tituloR.Nome
            };

            //Criação da liquidação direta
            var liquidacao = new Liquidacao()
            {
                Valor = (decimal)tituloR.Valor,
                FormaLiquidacao = FormaLiquidacao.Boleto,
                Data = DateTime.Now.Date,
                JurosMulta = 0,
                Desconto = 0
            };

            titulo.Liquidacoes.Add(liquidacao);
            new RepositorioPadrao<Titulo>().Incluir(titulo);
            TempData["Alerta"] = new Alerta() { Mensagem = "Título liquidado com sucesso", Tipo = "success" };


            return RedirectToAction("Index", Util.NomeControllerAnterior());
        }

        [Permissao]
        public ActionResult GerarBoleto(int tituloRecorrenteId, decimal valor, DateTime dataVencimento, int modeloBoletoId)
        {
            var boletobancario = Util.GerarBoletoBancario(tituloRecorrenteId, valor, dataVencimento, modeloBoletoId);
            ViewBag.BoletoBancario = boletobancario.MontaHtmlEmbedded();
            var modeloBoleto = new RepositorioModeloBoleto().BuscarPorId(modeloBoletoId);
            ViewBag.TituloRecorrenteId = tituloRecorrenteId;
            ViewBag.Valor = valor;
            ViewBag.DataVencimento = dataVencimento;
            ViewBag.Email = new RepositorioTituloRecorrente().BuscarPorId(tituloRecorrenteId).Pessoa.EmailFinanceiro;
            return View("~/Views/Recebimento/GerarBoleto.cshtml", modeloBoleto);
        }

        //private void ViewModelToModel(TituloViewModel viewModel, Titulo model)
        //{
        //    model.Id = 0;
        //    model.CategoriaId = viewModel.CategoriaId;
        //    model.CentroCustoId = viewModel.CentroCustoId;
        //    model.ContaId = viewModel.ContaId; //Valor não estava vindo, fiz um update no js da view
        //    model.DataVencimento = viewModel.DataVencimento;
        //    model.Descricao = viewModel.Descricao;
        //    model.TituloRecorrenteId = viewModel.Id;
        //    model.PessoaId = viewModel.PessoaId;
        //    model.Valor = viewModel.Valor != null ? (decimal)viewModel.Valor : viewModel.Liquidacoes.Sum(m => m.Valor);

        //    foreach (var l in viewModel.Liquidacoes)
        //    {
        //        model.Liquidacoes.Add(new Liquidacao()
        //        {
        //            Data = l.Data,
        //            Valor = l.Valor,
        //            JurosMulta = l.JurosMulta,
        //            FormaLiquidacao = l.FormaLiquidacao,
        //            TituloId = viewModel.Id,
        //            Desconto = l.Desconto
        //        });
        //    }
        //}
    }
}