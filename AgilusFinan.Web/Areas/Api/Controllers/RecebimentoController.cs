using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Areas.Api.Bases;
using AgilusFinan.Web.Controllers;
using AgilusFinan.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var response = client.DownloadString("https://api.iugu.com/v1/invoices/" + iuguId);
                var js = new JavaScriptSerializer();
                var faturaResponse = js.Deserialize<FaturaResponse>(response);
                return faturaResponse;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<string> SegundaVia(string cpfCnpj, string token)
        {
            //Response.ContentType = "application/json";

            try
            {
                TokenController.ValidarToken(token);
            }
            catch
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return "token não encontrado";
            }

            if (ValidaCpfCnpj(cpfCnpj))
            {
                cpfCnpj = Regex.Replace(cpfCnpj, "[^0-9]", "");
                try
                {
                    return await new FaturaGeradaController().SegundaViaCnpj(cpfCnpj);
                }
                catch (Exception ex)
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return ex.Message;
                }
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return "cnpj inválido ou vazio";
            }
        }

        private bool ValidaCpfCnpj(string cpfCnpj)
        {
            if (String.IsNullOrWhiteSpace(cpfCnpj))
                return false;

            cpfCnpj = Regex.Replace(cpfCnpj, "[^0-9]", "");
            
            if ((cpfCnpj.Length == 11 & cpfCnpj == "00000000000") || (cpfCnpj.Length == 14 & cpfCnpj == "00000000000000"))
                return false;

            //valida se for CPF
            if (cpfCnpj.Length == 11)
            {
                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string digito;
                int soma = 0;
                int resto;

                string tempCpf = cpfCnpj.Substring(0, 9);

                for (int i = 0; i < 9; i++)
                {
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                }

                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = resto.ToString();

                tempCpf += digito;

                soma = 0;
                for (int i = 0; i < 10; i++)
                {
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                }

                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito += resto.ToString();

                return cpfCnpj.EndsWith(digito);
            }

            //valida se for CNPJ
            if (cpfCnpj.Length == 14)
            {
                int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int soma = 0;
                int resto;
                string digito;
                string tempCnpj = cpfCnpj.Substring(0, 12);

                for (int i = 0; i < 12; i++)
                {
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
                }

                resto = soma % 11;
                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                }

                digito = resto.ToString();

                tempCnpj += digito;
                
                soma = 0;
                for (int i = 0; i < 13; i++)
                {
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
                }

                resto = soma % 11;

                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                }

                digito += resto.ToString();

                return cpfCnpj.EndsWith(digito);
            }

            return false;
        }
    }
}