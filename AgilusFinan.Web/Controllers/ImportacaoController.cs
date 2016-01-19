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

namespace AgilusFinan.Web.Controllers
{
    public class ImportacaoController : ControllerViewModelPadrao<Titulo, RepositorioRecebimento, TituloViewModel>
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


        public ActionResult ConcilarTitulos(string titulosAConciliar)
        {
            //magic happens

            DataTable tabelaTitulosNaoConciliados = new DataTable();
            tabelaTitulosNaoConciliados.Columns.Add("id_titulo", typeof(int));
            tabelaTitulosNaoConciliados.Columns.Add("valor", typeof(double));

            List<TituloPendente> titulosNaoConciliados = new List<TituloPendente>(); //Objeto criado para o Json


            foreach (var titulo in titulosNaoConciliados)
	        {
                tabelaTitulosNaoConciliados.Rows.Add(titulo.TituloId, titulo.Valor);
	        }

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