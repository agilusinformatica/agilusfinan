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
using System.Web.Script.Serialization;
using System.Net;
using System.Collections.Specialized;

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
            Session.Add("dataInicial", dataInicial);
            Session.Add("dataFinal", dataFinal);

            var parametros = new Dictionary<string, string>();
            parametros.Add("empresaId", UsuarioLogado.EmpresaId.ToString());
            parametros.Add("dataInicial", dataInicial.ToString());
            parametros.Add("dataFinal", dataFinal.ToString());
            var pagina = (PartialViewResult)Cache.Busca("recebimento", parametros);

            if (pagina == null)
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

                List<Titulo> titulosRecebimento = new List<Titulo>();
                titulosRecebimento = repo.Listar(t => t.DataVencimento >= dI && t.DataVencimento <= dF);

                //Trazendo também os títulos virtuais
                var titulosPendentes = GeradorTitulosPendentes.ChamarProcedimento(dI, dF, null);
                foreach (var tp in titulosPendentes)
                {
                    if (tp.Direcao == DirecaoCategoria.Recebimento && tp.TituloId == null)
                    {
                        titulosRecebimento.Add(
                           new Titulo()
                           {
                               Id = 0,
                               Descricao = tp.Descricao,
                               CategoriaId = tp.CategoriaId,
                               Categoria = new Categoria() { Nome = tp.NomeCategoria },
                               CentroCustoId = tp.CentroCustoId,
                               CentroCusto = new CentroCusto() { Nome = tp.NomeCentroCusto },
                               ContaId = tp.ContaId,
                               Conta = new Conta() { Nome = tp.NomeConta },
                               DataVencimento = tp.DataVencimento,
                               EmpresaId = UsuarioLogado.EmpresaId,
                               PessoaId = tp.PessoaId,
                               Pessoa = new Pessoa() { Nome = tp.NomePessoa },
                               TituloRecorrenteId = tp.TituloRecorrenteId,
                               Valor = tp.Valor == null ? 0 : (decimal)tp.Valor
                           }
                       );
                    }
                }
                pagina = PartialView("~/Views/Titulo/IndexData.cshtml", titulosRecebimento);
                Cache.Insere("recebimento", parametros, pagina);
            }
            return pagina;
        }


        protected override void PreInclusao()
        {
            base.PreInclusao();
            ViewBag.ContaId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome", new RepositorioConta().Listar(x => x.Padrao).Any() ? new RepositorioConta().Listar(x => x.Padrao).Single().Id : 0);
            ViewBag.CategoriaId = new SelectList(new RepositorioCategoria().Listar().Where(c => c.Direcao == DirecaoCategoria.Recebimento), "Id", "Nome");
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

        [HttpGet]
        [Permissao]
        public ActionResult Liquidar(int id)
        {
            var titulo = repo.BuscarPorId(id);
            var tituloVm = new TituloViewModel();
            ViewBag.ContaId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome", titulo.ContaId);
            tituloVm.FromModel(titulo);
            ViewBag.TipoTitulo = "Recebimento";
            ViewBag.ControllerRetorno = Util.NomeControllerAnterior();

            return PartialView("~/Views/" + FolderViewName() + "/_Liquidar.cshtml", tituloVm);
        }

        [HttpPost]
        [Permissao]
        public void Liquidar(string postedData)
        {
            Edit(postedData);
        }

        [Permissao]
        public ActionResult LiquidarDiretamente(int id)
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
                        TituloId = titulo.Id,
                        Desconto = 0
                    }
                );
                repo.Alterar(titulo);
                return RedirectToAction("Index", Util.NomeControllerAnterior());
            }
            else
            {
                throw new Exception("Já existe pagamento para este título.");
            }
        }
        [Permissao]
        public ActionResult GerarBoleto(int tituloId, int modeloBoletoId)
        {
            var boletobancario = Util.GerarBoletoBancario(tituloId, modeloBoletoId);
            ViewBag.BoletoBancario = boletobancario.MontaHtmlEmbedded();
            ViewBag.TituloId = tituloId;
            var modeloBoleto = new RepositorioModeloBoleto().BuscarPorId(modeloBoletoId);
            ViewBag.Email = new RepositorioRecebimento().BuscarPorId(tituloId).Pessoa.EmailFinanceiro;

            return View(modeloBoleto);
        }
        [HttpGet]
        public ActionResult BaixarBoleto(int tituloId, int modeloBoletoId)
        {
            var boletobancario = Util.GerarBoletoBancario(tituloId, modeloBoletoId);

            string boletoHtml = boletobancario.MontaHtmlEmbedded(false, true);
            
            var ms = Util.StringToPdf(boletoHtml);
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=boleto" + tituloId.ToString() + ".pdf");
            return new FileStreamResult(ms, "application/pdf");
        }
        [HttpGet]
        public ActionResult BaixarBoletoRecorrente(int modeloBoletoId, int tituloRecorrenteId, decimal valor, DateTime dataVencimento)
        {
            var boletobancario = Util.GerarBoletoBancario((int)tituloRecorrenteId, (decimal)valor, (DateTime)dataVencimento, modeloBoletoId);

            string boletoHtml = boletobancario.MontaHtmlEmbedded(false, true);

            var ms = Util.StringToPdf(boletoHtml);

            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=boleto" + tituloRecorrenteId.ToString() + ".pdf");

            return new FileStreamResult(ms, "application/pdf");
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EnviarBoletoPorEmail(int? tituloId, int modeloBoletoId, int? TituloRecorrenteId, decimal? Valor, DateTime? DataVencimento, string emailDestinatario, string AssuntoEmail, string TextoEmail)
        {
            if (tituloId != null)
            {
                Util.EnviarBoletoPorEmail((int)tituloId, "boleto.pdf", modeloBoletoId, emailDestinatario, AssuntoEmail, TextoEmail);
            }
            else
            {
                Util.EnviarBoletoPorEmail((int)TituloRecorrenteId, "boleto.pdf", modeloBoletoId, (decimal)Valor, (DateTime)DataVencimento, emailDestinatario, AssuntoEmail, TextoEmail);
            }
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

        [HttpGet]
        [Permissao]
        public virtual ActionResult Duplicar(int id)
        {
            Titulo model = repo.BuscarPorId(id);
            ViewBag.TipoOperacao = "Incluindo";
            var viewModel = new TituloViewModel();
            viewModel.FromModel(model);
            ViewBag.TipoTitulo = "Recebimento";
            PreAlteracao(viewModel);
            viewModel.TituloRecorrenteId = null;
            return FolderViewName() == String.Empty ? View(viewModel) : View("~/Views/" + FolderViewName() + "/Duplicar.cshtml", viewModel);
        }

    }

}