using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using AgilusFinan.Web.ViewModels;
using BoletoNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using AgilusFinan.Domain.Utils;
using System.Globalization;

namespace AgilusFinan.Web.Controllers
{
    public class RecebimentoController : ControllerViewModelPadrao<Titulo, RepositorioRecebimento, TituloViewModel>
    {
        protected override string FolderViewName()
        {
            return "Titulo";
        }

        protected override IEnumerable<Titulo> Dados()
        {
            return new List<Titulo>();
        }

        public ActionResult IndexData(string dataInicial, string dataFinal)
        {
            DateTime dI, dF;
            if (String.IsNullOrEmpty(dataInicial) && String.IsNullOrEmpty(dataFinal))
            {
                dI = Util.PrimeiroDiaMes(DateTime.Today);
                dF = Util.UltimoDiaMes(DateTime.Today);
            }
            else
            {
                dI = DateTime.ParseExact(dataInicial, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dF = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            ViewBag.ModelosBoleto = new RepositorioModeloBoleto().Listar().ToList();
            GerarLista();
            return PartialView("~/Views/Titulo/IndexData.cshtml", repo.Listar(t => t.DataVencimento >= dI && t.DataVencimento <= dF));
        }


        protected override void PreInclusao()
        {
            base.PreInclusao();
            ViewBag.ContaId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome", new RepositorioConta().Listar(x => x.Padrao).Any() ? new RepositorioConta().Listar(x => x.Padrao).Single().Id : 0);
            ViewBag.CategoriaId = new SelectList(new RepositorioCategoria().Listar().Where(c => c.Direcao == DirecaoCategoria.Recebimento), "Id", "Nome");
            ViewBag.ListaCategorias = Util.CategoriasIdentadas(DirecaoCategoria.Recebimento);
            ViewBag.PessoaId = new SelectList(new RepositorioPessoa().Listar(), "Id", "Nome");
            ViewBag.CentroCustoId = new SelectList(new RepositorioCentroCusto().Listar(), "Id", "Nome");
            GerarLista();
        }

        protected override void PreAlteracao(TituloViewModel viewModel)
        {
            base.PreAlteracao(viewModel);
            ViewBag.ContaId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome", viewModel.ContaId);
            ViewBag.CategoriaId = new SelectList(new RepositorioCategoria().Listar().Where(c => c.Direcao == DirecaoCategoria.Recebimento), "Id", "Nome", viewModel.CategoriaId);
            ViewBag.PessoaId = new SelectList(new RepositorioPessoa().Listar(), "Id", "Nome", viewModel.PessoaId);
            ViewBag.ListaCategorias = Util.CategoriasIdentadas(DirecaoCategoria.Recebimento);
            ViewBag.CentroCustoId = new SelectList(new RepositorioCentroCusto().Listar(), "Id", "Nome", viewModel.CentroCustoId);
            GerarLista();
        }

        private void GerarLista()
        {
            ViewBag.TipoTitulo = "Recebimento";
        }

        protected override void PreListagem()
        {
            base.PreListagem();
            GerarLista();
        }

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

            foreach (var l in model.Liquidacoes)
            {
                viewModel.Liquidacoes.Add(new LiquidacaoViewModel()
                {
                    Id = l.Id,
                    Data = l.Data,
                    Valor = l.Valor,
                    JurosMulta = l.JurosMulta,
                    FormaLiquidacao = l.FormaLiquidacao
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
            model.Valor = viewModel.Valor > 0 ? (decimal)viewModel.Valor : 0 ;
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
                    TituloId = viewModel.Id
                });
            }
        }

        [HttpGet]
        [Permissao]
        public ActionResult Liquidar(int id, bool homeIndex)
        {
            var titulo = repo.BuscarPorId(id);
            var tituloVm = new TituloViewModel();
            ViewBag.ContaId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome", titulo.ContaId);
            ModelToViewModel(titulo, tituloVm);
            ViewBag.TipoTitulo = "Recebimento";
            
            return View("~/Views/" + FolderViewName() + "/Liquidar.cshtml", tituloVm);
        }

        [Permissao]
        public ActionResult LiquidarDiretamente(int id, bool homeIndex)
        {
            var titulo = repo.BuscarPorId(id);
            if (titulo.Liquidacoes.Count == 0)
            {
                titulo.Liquidacoes.Add(
                    new Liquidacao()
                    {
                        Data = DateTime.Now.Date,
                        Valor = titulo.Valor,
                        JurosMulta = 0,
                        FormaLiquidacao = FormaLiquidacao.Boleto,
                        TituloId = titulo.Id
                    }
                );
                repo.Alterar(titulo);
                if (homeIndex)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                    return RedirectToAction("Index");
            }
            else
            {
                throw new Exception("Já existe pagamento para este título.");
            }
        }

        public ActionResult GerarBoleto(int tituloId, int modeloBoletoId)
        {
            var boletobancario = Util.GerarBoleto(tituloId, modeloBoletoId);
            ViewBag.BoletoBancario = boletobancario.MontaHtmlEmbedded();
            ViewBag.TituloId = tituloId;

            return View();
        }

        [HttpPost]
        public ActionResult EnviarBoletoPorEmail(int tituloId, int modeloBoletoId)
        {
            Util.EnviarBoletoPorEmail(tituloId, Server.MapPath(@"~/App_Data/teste.pdf"), modeloBoletoId);
            return RedirectToAction("Index", "Recebimento");
        }

        public Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        [HttpPost]
        [Permissao]
        public void Liquidar(string postedData)
        {
            Edit(postedData);
        }

    }

}