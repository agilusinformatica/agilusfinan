using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using Newtonsoft.Json;
using System.Globalization;
using AgilusFinan.Web.ViewModels;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AgilusFinan.Web.Controllers
{
    //Classes usadas para encapsular os dados do JSon que serão importados do extrato para o banco de dados.
    public class Task
    {
        public string itemExtratoId { get; set; }
        public ConciliacaoExtrato itemExtrato { get; set; }
        public bool selecionado { get; set; }
        public bool isNew { get; set; }
        public List<TituloSelecionado> titulosSelecionados { get; set; }
        public List<TituloIncluido> titulosIncluidos { get; set; }

        public Task()
        {
            titulosSelecionados = new List<TituloSelecionado>();
            titulosIncluidos = new List<TituloIncluido>();
        }
    }

    public class TituloSelecionado
    {
        public int? TituloId { get; set; }
        public int? TituloRecorrenteId { get; set; }
        public string Descricao { get; set; }
        public string PessoaId { get; set; }
        public string ContaId { get; set; }
        public double Valor { get; set; }
        public int CategoriaId { get; set; }
        public string DataVencimento { get; set; }
        public double Acrescimo { get; set; }
        public double Desconto { get; set; }
    }

    public class TituloIncluido
    {
        public int ContaId { get; set; }
        public string DataVencimento { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public int CategoriaId { get; set; }
        public int PessoaId { get; set; }
        public int? CentroCustoId { get; set; }
        public string Competencia { get; set; }
        public string Observacao { get; set; }
        public double Acrescimo { get; set; }
        public double Desconto { get; set; }
    }

    public class ConciliacaoController : Controller
    {
        // GET: ConciliacaoExtrato
        [Permissao]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConciliacaoExtrato(HttpPostedFileBase file)
        {
            return View("ConciliacaoExtrato", Parser.InterpretarOfx(file.InputStream));
        }

        public PartialViewResult VinculoTitulos(string tituloAConciliar, string dataInicial, string dataFinal)
        {
            var titulo = JsonConvert.DeserializeObject<ConciliacaoExtrato>(tituloAConciliar);

            DirecaoCategoria direcao = titulo.TipoLancamento == TipoLancamento.Credito ? DirecaoCategoria.Recebimento : DirecaoCategoria.Pagamento;

            var dI = DateTime.ParseExact(dataInicial, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var dF = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var titulosPendentes = GeradorTitulosPendentes.ChamarProcedimento(dI, dF, null).Where(t => t.Direcao == direcao).ToList();

            return PartialView("_VinculoTitulos", titulosPendentes);
        }

        public PartialViewResult AbreCadastroTitulo(string tipoTitulo)
        {
            ViewBag.ContaId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome", new RepositorioConta().Listar(x => x.Padrao).Any() ? new RepositorioConta().Listar(x => x.Padrao).Single().Id : 0);
            ViewBag.ListaCategorias = Util.CategoriasIdentadas(DirecaoCategoria.Pagamento);
            ViewBag.PessoaId = new SelectList(new RepositorioPessoa().Listar(), "Id", "Nome");
            ViewBag.CentroCustoId = new SelectList(new RepositorioCentroCusto().Listar(), "Id", "Nome");
            return PartialView("_CadastroTitulo", new TituloViewModel() { TipoTitulo = tipoTitulo });
        }

        [HttpPost]
        [HandleError]
        public string ConciliarTitulos(string titulosAVincular)
        {
            List<Task> extratoConciliacao = null;
            try
            {
                extratoConciliacao = JsonConvert.DeserializeObject<List<Task>>(titulosAVincular);

                DataTable tabelaTitulosSemVinculo = new DataTable();
                DataTable tabelaTitulosNaoCriados = new DataTable();


                tabelaTitulosSemVinculo.Columns.Add("Id", typeof(int));
                tabelaTitulosSemVinculo.Columns.Add("TituloId", typeof(int));
                tabelaTitulosSemVinculo.Columns.Add("TituloRecorrenteId", typeof(int));
                tabelaTitulosSemVinculo.Columns.Add("Descricao", typeof(string));
                tabelaTitulosSemVinculo.Columns.Add("PessoaId", typeof(int));
                tabelaTitulosSemVinculo.Columns.Add("ContaId", typeof(int));
                tabelaTitulosSemVinculo.Columns.Add("Valor", typeof(double));
                tabelaTitulosSemVinculo.Columns.Add("CategoriaId", typeof(int));
                tabelaTitulosSemVinculo.Columns.Add("DataVencimento", typeof(DateTime));
                tabelaTitulosSemVinculo.Columns.Add("Acrescimo", typeof(double));
                tabelaTitulosSemVinculo.Columns.Add("Desconto", typeof(double));
                tabelaTitulosSemVinculo.Columns.Add("DataLancamento", typeof(DateTime));
                tabelaTitulosSemVinculo.Columns.Add("ConciliacaoExtratoId", typeof(string));


                tabelaTitulosNaoCriados.Columns.Add("Id", typeof(int));
                tabelaTitulosNaoCriados.Columns.Add("ContaId", typeof(int));
                tabelaTitulosNaoCriados.Columns.Add("DataVencimento", typeof(DateTime));
                tabelaTitulosNaoCriados.Columns.Add("Descricao", typeof(string));
                tabelaTitulosNaoCriados.Columns.Add("Valor", typeof(double));
                tabelaTitulosNaoCriados.Columns.Add("CategoriaId", typeof(int));
                tabelaTitulosNaoCriados.Columns.Add("PessoaId", typeof(int));
                tabelaTitulosNaoCriados.Columns.Add("CentroCustoId", typeof(int));
                tabelaTitulosNaoCriados.Columns.Add("Competencia", typeof(DateTime));
                tabelaTitulosNaoCriados.Columns.Add("Observacao", typeof(string));
                tabelaTitulosNaoCriados.Columns.Add("DataLancamento", typeof(DateTime));
                tabelaTitulosNaoCriados.Columns.Add("Acrescimo", typeof(double));
                tabelaTitulosNaoCriados.Columns.Add("Desconto", typeof(double));
                tabelaTitulosNaoCriados.Columns.Add("ConciliacaoExtratoId", typeof(string));

                int seq = 0;

                foreach (var extrato in extratoConciliacao)
                {
                    foreach (var titulo in extrato.titulosIncluidos)
                    {
                        tabelaTitulosNaoCriados.Rows.Add(++seq, titulo.ContaId, titulo.DataVencimento,
                            titulo.Descricao, titulo.Valor, titulo.CategoriaId, titulo.PessoaId, titulo.CentroCustoId, titulo.Competencia,
                            titulo.Observacao, extrato.itemExtrato.DataLancamento, titulo.Acrescimo, titulo.Desconto, extrato.itemExtrato.Id);
                    }

                    foreach (var vinculo in extrato.titulosSelecionados)
                    {
                        tabelaTitulosSemVinculo.Rows.Add(++seq, vinculo.TituloId, vinculo.TituloRecorrenteId, vinculo.Descricao, vinculo.PessoaId, vinculo.ContaId, vinculo.Valor, vinculo.CategoriaId,
                            DateTime.ParseExact(vinculo.DataVencimento, "dd/MM/yyyy", new CultureInfo("en-US")),
                            vinculo.Acrescimo, vinculo.Desconto, extrato.itemExtrato.DataLancamento, extrato.itemExtrato.Id);
                    }
                }
                VinculadorExtrato.VincularTitulos(tabelaTitulosSemVinculo, tabelaTitulosNaoCriados);
                TempData["Alerta"] = new Alerta() { Mensagem = "Extrato conciliado com sucesso", Tipo = "success" };
                
            }
            catch (Exception erro)
            {
                return erro.Message;
            }

            return String.Empty;
        }


    }

}


