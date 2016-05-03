using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Utils;
using AgilusFinan.Infra.Context;
using AgilusFinan.Infra.Services;
using BoletoNet;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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

        public static string NomeControllerAnterior()
        {
            return HttpContext.Current.Request.UrlReferrer.ToString().Split('/')[3];
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

        public static Boleto GerarBoleto(int tituloId, int modeloBoletoId)
        {
            #region Instanciações
            var titulo = new RepositorioRecebimento().BuscarPorId(tituloId);
            var conta = titulo.Conta;
            var pessoa = titulo.Pessoa;
            var empresa = titulo.Empresa;
            int numeroBanco = conta.Banco.Codigo;
            var repoModeloBoleto = new RepositorioModeloBoleto();
            var dataVencimentoOriginal = titulo.DataVencimento;

            var modeloBoleto = repoModeloBoleto.BuscarPorId(modeloBoletoId);

            if (modeloBoleto == null)
            {
                throw new Exception("Modelo de Boleto não definido");
            }
            #endregion

            #region Gravar Boleto
            var repoBoletoGerado = new RepositorioBoletoGerado();
            var boletogerado = repoBoletoGerado.Listar(b => b.TituloId == tituloId).FirstOrDefault();

            if (boletogerado == null)
            {
                boletogerado = new BoletoGerado();
                boletogerado.ModeloBoletoId = modeloBoletoId;
                boletogerado.TituloId = tituloId;
                boletogerado.TituloRecorrenteId = null;

                modeloBoleto.NossoNumero++;
                repoModeloBoleto.Alterar(modeloBoleto);
                boletogerado.NossoNumero = modeloBoleto.NossoNumero;
                repoBoletoGerado.Incluir(boletogerado);
            }

            #endregion

            #region Cedente
            var c = new Cedente(empresa.CpfCnpj, empresa.Nome, conta.Agencia, conta.ContaCorrente.Split('-')[0], conta.ContaCorrente.Split('-')[1]);
            c.Codigo = conta.ContaCorrente;
            #endregion

            #region Boleto
            Boleto boleto = new Boleto(titulo.DataVencimento, titulo.Valor, modeloBoleto.Carteira, boletogerado.NossoNumero.ToString(), c, new EspecieDocumento(numeroBanco));
            boleto.NumeroDocumento = tituloId.ToString();
            boleto.Banco = new BoletoNet.Banco(numeroBanco);
            #endregion

            #region Sacado
            boleto.Sacado = new Sacado(pessoa.Cpf, pessoa.Nome);
            boleto.Sacado.Endereco = new BoletoNet.Endereco()
            {
                Bairro = pessoa.Endereco.Bairro,
                End = pessoa.Endereco.Logradouro + ", " + pessoa.Endereco.Numero + " " + pessoa.Endereco.Complemento,
                CEP = pessoa.Endereco.Cep,
                Cidade = pessoa.Endereco.Cidade,
                Complemento = pessoa.Endereco.Complemento,
                Numero = pessoa.Endereco.Numero,
                UF = pessoa.Endereco.Uf
            };
            #endregion

            #region Instruções
            Instrucao item1 = new Instrucao(numeroBanco);
            item1.Descricao = modeloBoleto.Instrucao;
            boleto.Instrucoes.Add(item1);
            #endregion

            #region Juros
            if (modeloBoleto.Juros > 0)
            {
                Instrucao item2 = new Instrucao(numeroBanco);
                decimal juros = boleto.ValorBoleto * modeloBoleto.Juros / 100 / 30;
                item2.Descricao = "Após o vencimento cobrar juros de R$ " + Math.Round(juros, 2) + " ao dia";
                boleto.Instrucoes.Add(item2);
                if (titulo.DataVencimento < DateTime.Today)
                {
                    boleto.DataVencimento = DateTime.Today;
                    int diasTotais = (int)(DateTime.Today - titulo.DataVencimento).TotalDays;
                    boleto.JurosMora = Math.Round(juros, 2) * diasTotais;
                }
            }
            #endregion

            #region Multa
            if (modeloBoleto.Multa > 0)
            {
                Instrucao item3 = new Instrucao(numeroBanco);
                decimal multa = boleto.ValorBoleto * modeloBoleto.Multa / 100;
                item3.Descricao = "Após o vencimento cobrar multa de R$ " + Math.Round(multa, 2);
                boleto.Instrucoes.Add(item3);
                if (titulo.DataVencimento < DateTime.Today)
                {
                    boleto.DataVencimento = DateTime.Today;
                    boleto.ValorMulta = multa;
                    Instrucao item4 = new Instrucao(conta.Banco.Codigo);
                    item4.Descricao = "Valor original: R$ " + boleto.ValorBoleto;
                    boleto.Instrucoes.Add(item4);
                }
            }

            boleto.ValorBoleto += boleto.JurosMora + boleto.ValorMulta;
            boleto.DataJurosMora = titulo.DataVencimento;
            boleto.DataMulta = titulo.DataVencimento;
            boleto.PercMulta = modeloBoleto.Multa;
            boleto.PercJurosMora = modeloBoleto.Juros;
            #endregion

            #region Desconto
            if (modeloBoleto.PercentualDesconto > 0)
            {
                boleto.DataDesconto = dataVencimentoOriginal.AddDays(-modeloBoleto.DiasDesconto);
                if (DateTime.Today <= boleto.DataDesconto)
                {
                    boleto.ValorDesconto = boleto.ValorBoleto * (modeloBoleto.PercentualDesconto / 100);
                    Instrucao instrucaoDesconto = new Instrucao(numeroBanco);
                    instrucaoDesconto.Descricao = "Até " + boleto.DataDesconto.GetDateTimeFormats()[0] + " conceder desconto de R$ " + Math.Round(boleto.ValorDesconto, 2);
                    boleto.Instrucoes.Add(instrucaoDesconto);
                }
            }

            #endregion

            return boleto;
        }

        public static Boleto GerarBoleto(int tituloRecorrenteId, decimal valor, DateTime dataVencimento, int modeloBoletoId)
        {
            #region Instancioações
            var titulo = new RepositorioTituloRecorrente().BuscarPorId(tituloRecorrenteId);
            var conta = titulo.Conta;
            var pessoa = titulo.Pessoa;
            var empresa = titulo.Empresa;
            int numeroBanco = conta.Banco.Codigo;
            var repoModeloBoleto = new RepositorioModeloBoleto();
            var modeloBoleto = repoModeloBoleto.BuscarPorId(modeloBoletoId);
            var dataVencimentoOriginal = dataVencimento;

            if (modeloBoleto == null)
            {
                throw new Exception("Modelo de Boleto não definido");
            }
            #endregion

            #region Gravar Boleto
            var repoBoletoGerado = new RepositorioBoletoGerado();
            var boletogerado = repoBoletoGerado.Listar(b => b.TituloRecorrenteId == tituloRecorrenteId && b.DataVencimento == dataVencimento).FirstOrDefault();

            if (boletogerado == null)
            {
                boletogerado = new BoletoGerado();
                boletogerado.ModeloBoletoId = modeloBoletoId;
                boletogerado.TituloId = null;
                boletogerado.TituloRecorrenteId = tituloRecorrenteId;
                boletogerado.DataVencimento = dataVencimento;

                modeloBoleto.NossoNumero++;
                repoModeloBoleto.Alterar(modeloBoleto);
                boletogerado.NossoNumero = modeloBoleto.NossoNumero;
                repoBoletoGerado.Incluir(boletogerado);
            }
            #endregion
             
            #region Cedente
            var c = new Cedente(empresa.CpfCnpj, empresa.Nome, conta.Agencia, conta.ContaCorrente.Split('-')[0], conta.ContaCorrente.Split('-')[1]);
            c.Codigo = conta.ContaCorrente;
            #endregion

            #region Boleto
            Boleto boleto = new Boleto(dataVencimento, valor, modeloBoleto.Carteira, boletogerado.NossoNumero.ToString(), c, new EspecieDocumento(numeroBanco));
            boleto.NumeroDocumento = tituloRecorrenteId.ToString();
            boleto.Banco = new BoletoNet.Banco(numeroBanco);
            #endregion

            #region Instrucoes
            Instrucao item1 = new Instrucao(conta.Banco.Codigo);
            item1.Descricao = modeloBoleto.Instrucao;
            boleto.Instrucoes.Add(item1);
            #endregion

            #region Juros
            if (modeloBoleto.Juros > 0)
            {
                Instrucao item2 = new Instrucao(conta.Banco.Codigo);
                decimal juros = boleto.ValorBoleto * modeloBoleto.Juros / 100 / 30;
                item2.Descricao = "Após o vencimento cobrar juros de R$ " + Math.Round(juros, 2) + " ao dia";
                boleto.Instrucoes.Add(item2);
                if (dataVencimento < DateTime.Today && titulo.Categoria.DirecaoVencimentoDiaNaoUtil == DirecaoVencimento.Antecipado)
                {
                    boleto.DataVencimento = DateTime.Today;
                    int diasTotais = (int)(DateTime.Today - dataVencimento).TotalDays;
                    boleto.JurosMora = Math.Round(juros, 2) * diasTotais;
                }
                else if (dataVencimento < DateTime.Today && titulo.Categoria.DirecaoVencimentoDiaNaoUtil == DirecaoVencimento.Prorrogado)
                {
                    boleto.DataVencimento = DateTime.Today;
                    int diasTotais = (int)(DateTime.Today - dataVencimento).TotalDays;
                    boleto.JurosMora = Math.Round(juros, 2) * diasTotais;
                }
            }
            #endregion

            #region Multa
            if (modeloBoleto.Multa > 0)
            {
                Instrucao item3 = new Instrucao(conta.Banco.Codigo);
                decimal multa = boleto.ValorBoleto * modeloBoleto.Multa / 100;
                item3.Descricao = "Após o vencimento cobrar multa de R$ " + Math.Round(multa, 2);
                boleto.Instrucoes.Add(item3);
                if (dataVencimento < DateTime.Today)
                {
                    boleto.DataVencimento = DateTime.Today;
                    boleto.ValorMulta = multa;
                    Instrucao item4 = new Instrucao(conta.Banco.Codigo);
                    item4.Descricao = "Valor original: R$ " + boleto.ValorBoleto;
                    boleto.Instrucoes.Add(item4);
                }
            }

            boleto.ValorBoleto += boleto.JurosMora + boleto.ValorMulta;
            boleto.DataJurosMora = dataVencimento;
            boleto.DataMulta = dataVencimento;
            boleto.PercMulta = modeloBoleto.Multa;
            boleto.PercJurosMora = modeloBoleto.Juros;
            #endregion

            #region Desconto
            if (modeloBoleto.PercentualDesconto > 0)
            {
                boleto.DataDesconto = dataVencimentoOriginal.AddDays(-modeloBoleto.DiasDesconto);
                if (DateTime.Today <= boleto.DataDesconto)
                {
                    boleto.ValorDesconto = boleto.ValorBoleto * (modeloBoleto.PercentualDesconto / 100);
                    Instrucao instrucaoDesconto = new Instrucao(numeroBanco);
                    instrucaoDesconto.Descricao = "Até " + boleto.DataDesconto.GetDateTimeFormats()[0] + " conceder desconto de R$ " + Math.Round(boleto.ValorDesconto, 2);
                    boleto.Instrucoes.Add(instrucaoDesconto);
                }
            }
            #endregion

            #region Sacado
            boleto.Sacado = new Sacado(pessoa.Cpf, pessoa.Nome);
            boleto.Sacado.Endereco = new BoletoNet.Endereco()
            {
                Bairro = pessoa.Endereco.Bairro,
                End = pessoa.Endereco.Logradouro + ", " + pessoa.Endereco.Numero + " " + pessoa.Endereco.Complemento,
                CEP = pessoa.Endereco.Cep,
                Cidade = pessoa.Endereco.Cidade,
                Complemento = pessoa.Endereco.Complemento,
                Numero = pessoa.Endereco.Numero,
                UF = pessoa.Endereco.Uf
            };
            #endregion

            return boleto;
        }

        public static BoletoBancario GerarBoletoBancario(int tituloId, int modeloBoletoId)
        {
            #region Boleto Bancario
            var boleto = GerarBoleto(tituloId, modeloBoletoId);
            var boletobancario = new BoletoBancario();
            boletobancario.CodigoBanco = (short)boleto.Banco.Codigo;
            boletobancario.Boleto = boleto;
            boletobancario.Boleto.Valida();
            #endregion

            return boletobancario;
        }

        public static BoletoBancario GerarBoletoBancario(int tituloRecorrenteId, decimal valor, DateTime dataVencimento, int modeloBoletoId)
        {
            #region Boleto Bancario
            var boleto = GerarBoleto(tituloRecorrenteId, valor, dataVencimento, modeloBoletoId);
            var boletobancario = new BoletoBancario();
            boletobancario.CodigoBanco = (short)boleto.Banco.Codigo;
            boletobancario.Boleto = boleto;
            boletobancario.OcultarEnderecoSacado = false;
            boletobancario.Boleto.Valida();
            #endregion

            return boletobancario;
        }

        public static void EnviarBoletoPorEmail(int tituloId, string nomeArquivo, int modeloBoletoId, string emailDestinatario, string AssuntoEmail, string TextoEmail)
        {
            var titulo = new RepositorioRecebimento().BuscarPorId(tituloId);
            var emailRemetente = titulo.Empresa.EmailFinanceiro;

            var boleto = Util.GerarBoletoBancario(tituloId, modeloBoletoId);
            var html = StringToStream(boleto.MontaHtmlEmbedded());
            var anexos = new List<Stream>();
            anexos.Add(html);
            var email = new Email(emailDestinatario, TextoEmail, AssuntoEmail, emailRemetente, anexos, new List<string>() { Path.GetFileName(nomeArquivo) });
            email.DispararMensagem();
        }

        public static void EnviarBoletoPorEmail(int tituloRecorrenteId, string nomeArquivo, int modeloBoletoId, decimal valor, DateTime dataVencimento, string emailDestinatario, string AssuntoEmail, string TextoEmail)
        {
            var titulo = new RepositorioTituloRecorrente().BuscarPorId(tituloRecorrenteId);
            var emailRemetente = titulo.Empresa.EmailFinanceiro;

            var boleto = Util.GerarBoletoBancario(tituloRecorrenteId, valor, dataVencimento, modeloBoletoId);
            var html = StringToStream(boleto.MontaHtmlEmbedded());
            var anexos = new List<Stream>();
            anexos.Add(html);
            var email = new Email(emailDestinatario, TextoEmail, AssuntoEmail, emailRemetente, anexos, new List<string>() { Path.GetFileName(nomeArquivo) });
            email.DispararMensagem();
        }

          
        public static void EnviarBoletoPorEmail(LoteBoleto loteBoleto, string nomeArquivo)
        {
            string emailDestinatario = "";
            string emailRemetente = "";
            BoletoBancario boleto = null;
            var modeloBoleto = new RepositorioModeloBoleto().BuscarPorId(loteBoleto.ModeloBoletoId);

            if (loteBoleto.TituloId != null)
            {
                var titulo = new RepositorioRecebimento().BuscarPorId((int)loteBoleto.TituloId);
                emailDestinatario = loteBoleto.EmailDestinatario;
                emailRemetente = titulo.Empresa.EmailFinanceiro;
                boleto = Util.GerarBoletoBancario((int)loteBoleto.TituloId, loteBoleto.ModeloBoletoId);
            }
            if (loteBoleto.TituloRecorrenteId != null)
            {
                var titulo = new RepositorioTituloRecorrente().BuscarPorId((int)loteBoleto.TituloRecorrenteId);
                emailDestinatario = loteBoleto.EmailDestinatario;
                emailRemetente = titulo.Empresa.EmailFinanceiro;
                boleto = Util.GerarBoletoBancario((int)loteBoleto.TituloRecorrenteId, loteBoleto.Valor, loteBoleto.DataVencimento, loteBoleto.ModeloBoletoId);
            }

            var html = StringToStream(boleto.MontaHtmlEmbedded());
            var anexos = new List<Stream>();
            anexos.Add(html);
            var email = new Email(emailDestinatario, modeloBoleto.TextoEmail, modeloBoleto.AssuntoEmail, emailRemetente, anexos, new List<string>() { Path.GetFileName(nomeArquivo) });
            email.DispararMensagem();

        }

        public static DateTime PrimeiroDiaMes(DateTime data)
        {
            return data.AddDays(-data.Day).AddDays(1).Date;
        }

        public static DateTime UltimoDiaMes(DateTime data)
        {
            return PrimeiroDiaMes(data).AddMonths(1).AddDays(-1).Date;
        }

        public static Stream SalvarBoleto(int tituloId, int modeloBoletoId)
        {
            var boleto = Util.GerarBoletoBancario(tituloId, modeloBoletoId);
            return StringToStream(boleto.MontaHtmlEmbedded());
        }

        public static Stream SalvarBoleto(int tituloRecorrenteId, decimal valor, DateTime dataVencimento, int modeloBoletoId)
        {
            var boleto = Util.GerarBoletoBancario(tituloRecorrenteId, valor, dataVencimento, modeloBoletoId);
            return StringToStream(boleto.MontaHtmlEmbedded());
        }

        private static Stream StringToStream(string src)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(src);
            return new MemoryStream(byteArray);
        }

        public static Color CorContrastante(Color cor)
        {
            int r = cor.R * 2;
            int g = cor.G * 5;
            int b = cor.B;

            if ((r + g + b) < 1024)
                return Color.White;
            else
                return Color.Black;


        }

    }
}
