using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Areas.Api.Bases;
using AgilusFinan.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AgilusFinan.Web.Areas.Api.Controllers
{
    public class RecebimentoController : ControllerViewModelApiPadrao<Titulo, RepositorioRecebimento, TituloViewModel>
    {
        [HttpPost]
        [AllowAnonymous]
        public void FaturaPaga(FaturaPaga data)
        {
            string id = data.id;
            var db = new Contexto();
            
            var faturaGerada = db.FaturasGeradas.Where(x => x.IuguId == id).FirstOrDefault();
            UsuarioLogado.EmpresaId = faturaGerada.EmpresaId;
            var faturaResponse = BuscarFaturaIUGU(faturaGerada.IuguId);

            if (faturaGerada.TituloId != null)
            {
                var repo = new RepositorioRecebimento();
                var titulo = repo.BuscarPorId((int)faturaGerada.TituloId);

                if (titulo.Liquidacoes.Count == 0)
                {
                    titulo.Liquidacoes.Add(
                        new Liquidacao()
                        {
                            Data = DateTime.Parse(faturaResponse.paid_at.Substring(0,10)).AddDays(2),
                            Valor = (decimal)faturaResponse.items_total_cents / 100,
                            JurosMulta = faturaResponse.items_total_cents < faturaResponse.total_paid_cents ? (decimal)(faturaResponse.total_paid_cents - faturaResponse.items_total_cents) / 100 : 0,
                            FormaLiquidacao = FormaLiquidacao.Boleto,
                            TituloId = titulo.Id,
                            Desconto = faturaResponse.items_total_cents > faturaResponse.total_paid_cents ? (decimal)(faturaResponse.items_total_cents - faturaResponse.total_paid_cents) / 100 : 0
                        }
                    );
                    repo.Alterar(titulo);
                }
            }
            else
            {
                var repo = new RepositorioTituloRecorrente();

                var tituloRecorrente = repo.BuscarPorId((int)faturaGerada.TituloRecorrenteId);
                TituloViewModel titulo = new TituloViewModel();

                titulo.CategoriaId = tituloRecorrente.CategoriaId;
                titulo.CentroCustoId = tituloRecorrente.CentroCustoId;
                titulo.ContaId = tituloRecorrente.ContaId;
                titulo.DataVencimento = faturaGerada.DataVencimento;
                titulo.PessoaId = tituloRecorrente.PessoaId;
                titulo.Valor = (decimal)faturaResponse.items_total_cents / 100;
                titulo.TituloRecorrenteId = tituloRecorrente.Id;
                titulo.Descricao = tituloRecorrente.Nome;
                titulo.Observacao = tituloRecorrente.Observacao;

                var liquidacao = new LiquidacaoViewModel()
                    {
                        Data = DateTime.Parse(faturaResponse.paid_at.Substring(0, 10)),
                        Valor = (decimal)faturaResponse.items_total_cents / 100,
                        JurosMulta = faturaResponse.items_total_cents < faturaResponse.total_paid_cents ? (decimal)(faturaResponse.total_paid_cents - faturaResponse.items_total_cents) / 100 : 0,
                        FormaLiquidacao = FormaLiquidacao.Boleto,
                        Desconto = faturaResponse.items_total_cents > faturaResponse.total_paid_cents ? (decimal)(faturaResponse.items_total_cents - faturaResponse.total_paid_cents) / 100 : 0
                    };

                titulo.Liquidacoes.Add(liquidacao);

                var novoTitulo = titulo.ToModel();
                RepositorioRecebimento repoTitulo = new RepositorioRecebimento();
                repoTitulo.Incluir(novoTitulo);
            }


        }

        public FaturaResponse BuscarFaturaIUGU (string iuguId){
            using (WebClient client = new WebClient())
            {

                client.Encoding = System.Text.Encoding.UTF8;
                var tokenIUGU = new RepositorioParametro().Listar().FirstOrDefault().TokenIUGU;

                client.Headers.Add("Content-Type", "application/json");
                client.Headers.Add("Authorization", "Basic " + tokenIUGU);

                var response = client.DownloadString("https://api.iugu.com/v1/invoices/" + iuguId);
                var js = new JavaScriptSerializer();
                var faturaResponse = js.Deserialize<FaturaResponse>(response);
                return faturaResponse;
            }
        }
    }
}