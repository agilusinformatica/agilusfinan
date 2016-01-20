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
        public bool selectionado { get; set; }
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
        public double Valor { get; set; }
        public DirecaoCategoria Direcao { get; set; }
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
        public int CentroCustoId { get; set; }
        public string Competencia { get; set; }
        public string Observacao { get; set; }
    }



    public class ImportacaoController : Controller
    {
        // GET: ConciliacaoExtrato
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConciliacaoExtrato(HttpPostedFileBase file)
        {
            ViewBag.ContaId = new SelectList(new RepositorioConta().Listar(), "Id", "Nome", new RepositorioConta().Listar(x => x.Padrao).Any() ? new RepositorioConta().Listar(x => x.Padrao).Single().Id : 0);
            ViewBag.ListaCategorias = Util.CategoriasIdentadas(DirecaoCategoria.Pagamento);
            ViewBag.PessoaId = new SelectList(new RepositorioPessoa().Listar(), "Id", "Nome");
            ViewBag.CentroCustoId = new SelectList(new RepositorioCentroCusto().Listar(), "Id", "Nome");
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


        [HttpPost]
        public ActionResult ConciliarTitulos(string titulosAVincular)
        {
            var extratoConciliacao = JsonConvert.DeserializeObject<List<Task>>(titulosAVincular);
            DataTable tabelaTitulosNaoConciliados = new DataTable();
            DataTable tabelaTitulosAIncluir = new DataTable();

            tabelaTitulosNaoConciliados.Columns.Add("id_titulo", typeof(int));
            tabelaTitulosNaoConciliados.Columns.Add("valor", typeof(double));

            /* foreach (var titulo in tabelaTitulosNaoConciliados)
             {
                 tabelaTitulosNaoConciliados.Rows.Add(titulo.TituloId, titulo.Valor);
             }

             foreach (var titulo in tabelaTitulosAIncluir)
             {
                 tabelaTitulosAIncluir.Rows.Add(titulo.TituloId, titulo.Valor);
             }*/

            //Responsabilidades do repositório

            var parameter = new SqlParameter("@titulosSemVinculo", SqlDbType.Structured);
            parameter.Value = tabelaTitulosNaoConciliados;
            parameter.TypeName = "dbo.TabelaVinculoDeExtrato";
            //db.Database.ExecuteSqlCommand("exec dbo.usp_SaveStudents @students", parameter);


            //then..
            return View("ConciliacaoExtrato"); //Pensar o que deve ser retornado no final do processo
        }

    }

}


