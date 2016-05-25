using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using AgilusFinan.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AgilusFinan.Web.Controllers
{
    public class PagamentoController : ControllerViewModelPadrao<Titulo, RepositorioPagamento, TituloViewModel>
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
            var pagina = (PartialViewResult)Cache.Busca("pagamento", parametros);

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
                GerarLista();

                List<Titulo> titulosPagamento = new List<Titulo>();
                titulosPagamento = repo.Listar(t => t.DataVencimento >= dI && t.DataVencimento <= dF);

                //Trazendo também os títulos virtuais
                var titulosPendentes = GeradorTitulosPendentes.ChamarProcedimento(dI, dF, null);
                foreach (var tp in titulosPendentes)
                {
                    if (tp.Direcao == DirecaoCategoria.Pagamento && tp.TituloId == null)
                    {
                        titulosPagamento.Add(
                           new Titulo()
                           {
                               Id = 0,
                               Descricao = tp.Descricao,
                               CategoriaId = tp.CategoriaId,
                               Categoria = new Categoria() { Nome = tp.NomeCategoria },
                               CentroCustoId = tp.CentroCustoId,
                               CentroCusto = new CentroCusto() { Nome = tp.NomeCentroCusto },
                               ContaId = tp.ContaId,
                               Conta = new Conta(){Nome = tp.NomeConta},
                               DataVencimento = tp.DataVencimento,
                               EmpresaId = UsuarioLogado.EmpresaId,
                               PessoaId = tp.PessoaId,
                               Pessoa = new Pessoa(){ Nome = tp.NomePessoa},
                               TituloRecorrenteId = tp.TituloRecorrenteId,
                               Valor = tp.Valor == null ? 0 : (decimal)tp.Valor
                           }
                       );
                    }
                }

                pagina = PartialView("~/Views/Titulo/IndexData.cshtml", titulosPagamento);
                Cache.Insere("pagamento", parametros, pagina);
            }

            return pagina;
        }

        protected override void PreInclusao()
        {
            base.PreInclusao();
            ViewBag.ContaId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome", new RepositorioConta().Listar(x => x.Padrao).Any() ? new RepositorioConta().Listar(x => x.Padrao).Single().Id : 0);
            ViewBag.PessoaId = new SelectList(new RepositorioPessoa().Listar(), "Id", "Nome");
            ViewBag.CentroCustoId = new SelectList(new RepositorioCentroCusto().Listar(), "Id", "Nome");
            GerarLista();
        }

        protected override void PreAlteracao(TituloViewModel viewModel)
        {
            base.PreAlteracao(viewModel);
            ViewBag.ContaId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome", viewModel.ContaId);
            ViewBag.PessoaId = new SelectList(new RepositorioPessoa().Listar(), "Id", "Nome", viewModel.PessoaId);
            ViewBag.CentroCustoId = new SelectList(new RepositorioCentroCusto().Listar(), "Id", "Nome", viewModel.CentroCustoId);
            GerarLista();
        }

        private void GerarLista()
        {
            ViewBag.TipoTitulo = "Pagamento";
        }

        protected override void PreListagem()
        {
            base.PreListagem();
            GerarLista();
        }

        //protected override void ModelToViewModel(Titulo model, TituloViewModel viewModel)
        //{
        //    base.ModelToViewModel(model, viewModel);
        //    viewModel.Id = model.Id;
        //    viewModel.CategoriaId = model.CategoriaId;
        //    viewModel.CentroCustoId = model.CentroCustoId;
        //    viewModel.PessoaId = model.PessoaId;
        //    viewModel.Competencia = model.Competencia;
        //    viewModel.ContaId = model.ContaId;
        //    viewModel.DataVencimento = model.DataVencimento;
        //    viewModel.Descricao = model.Descricao;
        //    viewModel.Valor = model.Valor;
        //    viewModel.Observacao = model.Observacao;
        //    viewModel.TituloRecorrenteId = model.TituloRecorrenteId;
        //    viewModel.TipoTitulo = "Pagamento";

        //    foreach (var l in model.Liquidacoes)
        //    {
        //        viewModel.Liquidacoes.Add(new LiquidacaoViewModel()
        //        {
        //            Id = l.Id,
        //            Data = l.Data,
        //            Valor = l.Valor,
        //            JurosMulta = l.JurosMulta,
        //            FormaLiquidacao = l.FormaLiquidacao,
        //            Desconto = l.Desconto
        //        });
        //    }
        //}

        //protected override void ViewModelToModel(TituloViewModel viewModel, Titulo model)
        //{
        //    base.ViewModelToModel(viewModel, model);
        //    model.Id = viewModel.Id;
        //    model.CategoriaId = viewModel.CategoriaId;
        //    model.CentroCustoId = viewModel.CentroCustoId;
        //    model.PessoaId = viewModel.PessoaId;
        //    model.Competencia = viewModel.Competencia;
        //    model.ContaId = viewModel.ContaId;
        //    model.DataVencimento = viewModel.DataVencimento;
        //    model.Descricao = viewModel.Descricao;
        //    model.Valor = viewModel.Valor > 0 ? (decimal)viewModel.Valor : 0;
        //    model.Observacao = viewModel.Observacao;
        //    model.TituloRecorrenteId = viewModel.TituloRecorrenteId;

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

        [HttpGet]
        [Permissao]
        public ActionResult Liquidar(int id)
        {
            var titulo = repo.BuscarPorId(id);
            var tituloVm = new TituloViewModel();
            ViewBag.ContaId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome", titulo.ContaId);
            ModelToViewModel(titulo, tituloVm);
            ViewBag.TipoTitulo = "Pagamento"; 
            ViewBag.ControllerRetorno = Util.NomeControllerAnterior();
            
            return View("~/Views/" + FolderViewName() + "/Liquidar.cshtml", tituloVm);
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
                TempData["Alerta"] = new Alerta() { Mensagem = "Título liquidado com sucesso", Tipo = "success" };

                return RedirectToAction("Index", Util.NomeControllerAnterior());
            }
            else
            {
                throw new Exception("Já existe pagamento para este título.");
            }
        }

        [HttpPost]
        [Permissao]
        public void Liquidar(string postedData)
        {
            Edit(postedData);
            TempData["Alerta"] = new Alerta() { Mensagem = "Título liquidado com sucesso", Tipo = "success" };
        }

        [HttpGet]
        [Permissao]
        public virtual ActionResult Duplicar(int id)
        {
            Titulo model = repo.BuscarPorId(id);
            ViewBag.TipoOperacao = "Incluindo";
            var viewModel = new TituloViewModel();
            ModelToViewModel(model, viewModel);
            PreAlteracao(viewModel);
            viewModel.TituloRecorrenteId = null;
            return FolderViewName() == String.Empty ? View(viewModel) : View("~/Views/" + FolderViewName() + "/Duplicar.cshtml", viewModel);
        }
    }
}