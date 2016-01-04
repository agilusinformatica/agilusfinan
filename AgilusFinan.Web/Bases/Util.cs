using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Utils;
using AgilusFinan.Infra.Context;
using AgilusFinan.Infra.Services;
using BoletoNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilusFinan.Web.Bases
{
    public static class Util
    {

        static private Dictionary<int, string> lista;
        static private List<Categoria> itensCategoria;
        static private List<Funcao> itensFuncao;

        private static void AdicionaItemCategoria(Categoria c, int nivel, int tamanhoIdentacao)
        {
            string identador = System.Net.WebUtility.HtmlDecode("&nbsp;");
            lista.Add(c.Id, Repete(identador, nivel * tamanhoIdentacao) + c.Nome);
            var filhas = itensCategoria.Where(f => f.CategoriaPaiId == c.Id && f.Id != f.CategoriaPaiId);
            foreach (var item in filhas)
            {
                AdicionaItemCategoria(item, ++nivel, tamanhoIdentacao);
                --nivel;
            }
        }

        public static Dictionary<int, string> CategoriasIdentadas(DirecaoCategoria? direcao, int tamanhoIdentacao = 3)
        {
            var repo = new RepositorioCategoria();

            if (direcao != null)
            {
                itensCategoria = repo.Listar(c => c.Direcao == direcao);
            }
            else
            {
                itensCategoria = repo.Listar().ToList();
            }

            lista = new Dictionary<int, string>();
            var root = itensCategoria.Where(c => c.CategoriaPaiId == null);
            foreach (var item in root)
            {
                AdicionaItemCategoria(item, 0, tamanhoIdentacao);
            }

            return lista;
        }

        private static void AdicionaItemFuncao(Funcao c, int nivel, int tamanhoIdentacao)
        {
            string identador = System.Net.WebUtility.HtmlDecode("&nbsp;");
            lista.Add(c.Id, Repete(identador, nivel * tamanhoIdentacao) + c.Descricao);
            var filhas = itensFuncao.Where(f => f.FuncaoSuperiorId == c.Id && f.Id != f.FuncaoSuperiorId);
            foreach (var item in filhas)
            {
                AdicionaItemFuncao(item, ++nivel, tamanhoIdentacao);
                --nivel;
            }
        }

        public static Dictionary<int, string> FuncoesIdentadas(int tamanhoIdentacao = 3)
        {
            itensFuncao = new Contexto().Funcoes.ToList();

            lista = new Dictionary<int, string>();
            var root = itensFuncao.Where(c => c.FuncaoSuperiorId == null);
            foreach (var item in root)
            {
                AdicionaItemFuncao(item, 0, tamanhoIdentacao);
            }

            return lista;
        }

        static private string Repete(string texto, int qtde)
        {
            string retorno = "";
            for (int i = 0; i < qtde; i++)
            {
                retorno += texto;
            }
            return retorno;
        }

        public static void EnviarConvite(Convite convite, int empresaId, string remetente)
        {
            string token = Criptografia.Encriptar(convite.Email + "|" + convite.PerfilId.ToString() + "|" + empresaId.ToString());
            var Email = new Email(convite.Email, EnderecoHost() + "/Login/EfetivarConvite?token=" + token, "Convite", remetente);
            Email.DispararMensagem();
        }

        public static string EnderecoHost()
        {
            return
                System.Web.HttpContext.Current.Request.Url.Scheme + @"://" +
                System.Web.HttpContext.Current.Request.Url.Host + ":" +
                System.Web.HttpContext.Current.Request.Url.Port.ToString();
        }


        public static BoletoBancario GerarBoleto(int tituloId, int modeloBoletoId)
        {
            var titulo = new RepositorioRecebimento().BuscarPorId(tituloId);
            var conta = titulo.Conta;
            var pessoa = titulo.Pessoa;
            var empresa = titulo.Empresa;
            int numeroBanco = conta.Banco.Codigo;
            var repoModeloBoleto = new RepositorioModeloBoleto();

            var modeloBoleto = repoModeloBoleto.BuscarPorId(modeloBoletoId);

            if (modeloBoleto == null)
            {
                throw new Exception("Modelo de Boleto não definido");
            }

            //Incremento do Nosso número em Modelo de Boleto
            modeloBoleto.NossoNumero++;
            repoModeloBoleto.Alterar(modeloBoleto);

            //Cedente
            var c = new Cedente(empresa.CpfCnpj, empresa.Nome, conta.Agencia, conta.ContaCorrente.Split('-')[0], conta.ContaCorrente.Split('-')[1]);
            c.Codigo = conta.ContaCorrente;

            //boleto
            Boleto boleto = new Boleto(titulo.DataVencimento, titulo.Valor, modeloBoleto.Carteira, modeloBoleto.NossoNumero.ToString(), c, new EspecieDocumento(numeroBanco));
            boleto.NumeroDocumento = "12345891";

            //Sacado
            boleto.Sacado = new Sacado(pessoa.Cpf, pessoa.Nome);
            boleto.Sacado.Endereco = new BoletoNet.Endereco()
            {
                Bairro = pessoa.Endereco.Bairro,
                Logradouro = pessoa.Endereco.Logradouro,
                CEP = pessoa.Endereco.Cep,
                Cidade = pessoa.Endereco.Cidade,
                Complemento = pessoa.Endereco.Complemento,
                Numero = pessoa.Endereco.Numero,
                UF = pessoa.Endereco.Uf
            };


            var boletobancario = new BoletoBancario();
            boletobancario.CodigoBanco = (short)conta.Banco.Codigo;
            boletobancario.Boleto = boleto;
            boletobancario.Boleto.Valida();



            return boletobancario;
        }

        public static BoletoBancario GerarBoleto(int tituloRecorrenteId, decimal valor, DateTime dataVencimento, int modeloBoletoId)
        {
            var titulo = new RepositorioTituloRecorrente().BuscarPorId(tituloRecorrenteId);
            var conta = titulo.Conta;
            var pessoa = titulo.Pessoa;
            var empresa = titulo.Empresa;
            int numeroBanco = conta.Banco.Codigo;
            var repoModeloBoleto = new RepositorioModeloBoleto();

            var modeloBoleto = repoModeloBoleto.BuscarPorId(modeloBoletoId);

            if (modeloBoleto == null)
            {
                throw new Exception("Modelo de Boleto não definido");
            }

            //Incremento do Nosso número em Modelo de Boleto
            modeloBoleto.NossoNumero++;
            repoModeloBoleto.Alterar(modeloBoleto);

            //Cedente
            var c = new Cedente(empresa.CpfCnpj, empresa.Nome, conta.Agencia, conta.ContaCorrente.Split('-')[0], conta.ContaCorrente.Split('-')[1]);
            c.Codigo = conta.ContaCorrente;

            //boleto
            Boleto boleto = new Boleto(dataVencimento, valor, modeloBoleto.Carteira, modeloBoleto.NossoNumero.ToString(), c, new EspecieDocumento(numeroBanco));
            boleto.NumeroDocumento = "12345891";

            //Sacado
            boleto.Sacado = new Sacado(pessoa.Cpf, pessoa.Nome);
            boleto.Sacado.Endereco = new BoletoNet.Endereco()
            {
                Bairro = pessoa.Endereco.Bairro,
                Logradouro = pessoa.Endereco.Logradouro,
                CEP = pessoa.Endereco.Cep,
                Cidade = pessoa.Endereco.Cidade,
                Complemento = pessoa.Endereco.Complemento,
                Numero = pessoa.Endereco.Numero,
                UF = pessoa.Endereco.Uf
            };


            var boletobancario = new BoletoBancario();
            boletobancario.CodigoBanco = (short)conta.Banco.Codigo;
            boletobancario.Boleto = boleto;
            boletobancario.Boleto.Valida();

            return boletobancario;
        }

        public static void EnviarBoletoPorEmail(int tituloId, string arquivoTemporario, int modeloBoletoId)
        {
            var titulo = new RepositorioRecebimento().BuscarPorId(tituloId);
            var emailDestinatario = titulo.Pessoa.EmailFinanceiro;
            var emailRemetente = titulo.Empresa.EmailFinanceiro;


            var boleto = Util.GerarBoleto(tituloId, modeloBoletoId);
            GeradorPdf.HtmlParaPdf(boleto.MontaHtmlEmbedded(false, true), arquivoTemporario);
            var anexos = new List<string>();
            anexos.Add(arquivoTemporario);
            var email = new Email(emailDestinatario, "Teste de envio de boleto pelo agilus finan", "Teste Boleto", emailRemetente, anexos);
            email.DispararMensagem();
            System.IO.File.Delete(arquivoTemporario);

        }

        public static void EnviarBoletoPorEmail(int tituloRecorrenteId, decimal valor, DateTime dataVencimento, string arquivoTemporario, int modeloBoletoId)
        {
            var titulo = new RepositorioTituloRecorrente().BuscarPorId(tituloRecorrenteId);
            var emailDestinatario = titulo.Pessoa.EmailFinanceiro;
            var emailRemetente = titulo.Empresa.EmailFinanceiro;


            var boleto = Util.GerarBoleto(tituloRecorrenteId, valor, dataVencimento, modeloBoletoId);
            GeradorPdf.HtmlParaPdf(boleto.MontaHtmlEmbedded(false, true), arquivoTemporario);
            var anexos = new List<string>();
            anexos.Add(arquivoTemporario);
            var email = new Email(emailDestinatario, "Teste de envio de boleto pelo agilus finan", "Teste Boleto", emailRemetente, anexos);
            email.DispararMensagem();
            System.IO.File.Delete(arquivoTemporario);
        }

        public static DateTime PrimeiroDiaMes(DateTime data)
        {
            return data.AddDays(-data.Day).AddDays(1).Date;
        }

        public static DateTime UltimoDiaMes(DateTime data)
        {
            return PrimeiroDiaMes(data).AddMonths(1).AddDays(-1).Date;
        }

    }
}
