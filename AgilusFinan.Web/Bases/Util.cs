using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Utils;
using AgilusFinan.Infra.Context;
using AgilusFinan.Infra.Services;
using BoletoNet;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
            
            string htmlemail = "<p style=\"text-align: center; line-height: 3;\"><img src=\"data:image/png;base64,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\" style=\"width: 333px; float: none;\"></p><p style=\"text-align: center; \"><span style=\"line-height: 18.5714px; text-align: start;\"><br></span></p><p style=\"text-align: center; line-height: 1.6;\"><span style=\"line-height: 18.5714px;\">Agora você poderá gerenciar suas contas&nbsp;com a ajuda do nosso sistema Financeiro.</span></p><p style=\"text-align: center; line-height: 1.6;\"><span style=\"line-height: 18.5714px;\">Clique&nbsp;<a href="+EnderecoHost() + "/Login/EfetivarConvite?token=" + token+">aqui&nbsp;</a>para finalizar seu cadastro.</span></p><p style=\"text-align: center; line-height: 1.6;\"><br></p><table class=\"table table-bordered\"><tbody></tbody></table>";
            var Email = new Email(convite.Email, htmlemail, "Convite", remetente);
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
            var db = new Contexto();
            var titulo = db.Titulos.Find(tituloId);
            var conta = titulo.Conta;
            var pessoa = titulo.Pessoa;
            var empresa = titulo.Empresa;
            int numeroBanco = conta.Banco.Codigo;
            var modeloBoleto = db.ModelosBoleto.Find(modeloBoletoId);
            var dataVencimentoOriginal = titulo.DataVencimento;
            Decimal jurosTotais = 0;

            if (modeloBoleto == null)
            {
                throw new Exception("Modelo de Boleto não definido");
            }
            #endregion

            #region Gravar Boleto
            var boletoGerado = db.BoletosGerado.Where(b => b.TituloId == tituloId && b.ModeloBoletoId == modeloBoletoId).FirstOrDefault();

            if (boletoGerado == null)
            {
                boletoGerado = new BoletoGerado();
                boletoGerado.ModeloBoletoId = modeloBoletoId;
                boletoGerado.TituloId = tituloId;
                boletoGerado.TituloRecorrenteId = null;
                boletoGerado.PercentualDesconto = modeloBoleto.PercentualDesconto;
                boletoGerado.DiasDesconto = modeloBoleto.DiasDesconto;
                boletoGerado.Juros = modeloBoleto.Juros;
                boletoGerado.Multa = modeloBoleto.Multa;

                modeloBoleto.NossoNumero++;
                db.Entry<ModeloBoleto>(modeloBoleto).State = System.Data.Entity.EntityState.Modified;
                boletoGerado.NossoNumero = modeloBoleto.NossoNumero;
                boletoGerado.EmpresaId = titulo.EmpresaId;
                db.BoletosGerado.Add(boletoGerado);
                db.SaveChanges();
            }

            #endregion

            #region Cedente
            var c = new Cedente(empresa.CpfCnpj, empresa.Nome, conta.Agencia, conta.ContaCorrente.Split('-')[0], conta.ContaCorrente.Split('-')[1]);
            c.Codigo = conta.ContaCorrente;
            #endregion

            #region Boleto
            Boleto boleto = new Boleto(titulo.DataVencimento, titulo.Valor, modeloBoleto.Carteira, boletoGerado.NossoNumero.ToString(), c, new EspecieDocumento(numeroBanco));
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
            if (boletoGerado.Juros > 0)
            {
                Instrucao item2 = new Instrucao(numeroBanco);
                boleto.JurosMora = Math.Round(boleto.ValorBoleto * boletoGerado.Juros / 100 / 30, 2);
                item2.Descricao = "Após o vencimento cobrar juros de R$ " + boleto.JurosMora + " ao dia";
                boleto.Instrucoes.Add(item2);
                if (titulo.DataVencimento < DateTime.Today)
                {
                    boleto.DataVencimento = DateTime.Today;
                    int diasTotais = (int)(DateTime.Today - titulo.DataVencimento).TotalDays;
                    jurosTotais = boleto.JurosMora * diasTotais;
                }
            }
            #endregion

            #region Multa
            if (boletoGerado.Multa > 0)
            {
                Instrucao item3 = new Instrucao(numeroBanco);
                decimal multa = boleto.ValorBoleto * boletoGerado.Multa / 100;
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

            boleto.ValorBoleto += jurosTotais + boleto.ValorMulta;
            boleto.DataJurosMora = titulo.DataVencimento;
            boleto.DataMulta = titulo.DataVencimento;
            boleto.PercMulta = boletoGerado.Multa;
            boleto.PercJurosMora = boletoGerado.Juros;
            #endregion

            #region Desconto
            if (boletoGerado.PercentualDesconto > 0)
            {
                boleto.DataDesconto = dataVencimentoOriginal.AddDays(-boletoGerado.DiasDesconto);
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
            #region Instanciações
            var db = new Contexto();
            //var titulo = new RepositorioTituloRecorrente().BuscarPorId(tituloRecorrenteId);
            var titulo = db.TitulosRecorrentes.Find(tituloRecorrenteId);
            var conta = titulo.Conta;
            var pessoa = titulo.Pessoa;
            var empresa = titulo.Empresa;
            int numeroBanco = conta.Banco.Codigo;
            //var repoModeloBoleto = new RepositorioModeloBoleto();
            //var modeloBoleto = repoModeloBoleto.BuscarPorId(modeloBoletoId);
            var modeloBoleto = db.ModelosBoleto.Find(modeloBoletoId);
            var dataVencimentoOriginal = dataVencimento;
            Decimal JurosTotais = 0;
            if (modeloBoleto == null)
            {
                throw new Exception("Modelo de Boleto não definido");
            }
            #endregion

            #region Gravar Boleto
            //var repoBoletoGerado = new RepositorioBoletoGerado();
            //var boletogerado = repoBoletoGerado.Listar(b => b.TituloRecorrenteId == tituloRecorrenteId && b.DataVencimento == dataVencimento).FirstOrDefault();
            var boletoGerado = db.BoletosGerado.Where(b => b.TituloRecorrenteId == tituloRecorrenteId && b.DataVencimento == dataVencimento).FirstOrDefault();

            if (boletoGerado == null)
            {
                boletoGerado = new BoletoGerado();
                boletoGerado.ModeloBoletoId = modeloBoletoId;
                boletoGerado.TituloId = null;
                boletoGerado.TituloRecorrenteId = tituloRecorrenteId;
                boletoGerado.DataVencimento = dataVencimento;
                boletoGerado.PercentualDesconto = modeloBoleto.PercentualDesconto;
                boletoGerado.DiasDesconto = modeloBoleto.DiasDesconto;
                boletoGerado.Juros = modeloBoleto.Juros;
                boletoGerado.Multa = modeloBoleto.Multa;

                modeloBoleto.NossoNumero++;
                //repoModeloBoleto.Alterar(modeloBoleto);
                db.Entry<ModeloBoleto>(modeloBoleto).State = System.Data.Entity.EntityState.Modified;
                boletoGerado.NossoNumero = modeloBoleto.NossoNumero;
                boletoGerado.EmpresaId = titulo.EmpresaId;
                //repoBoletoGerado.Incluir(boletoGerado);
                db.BoletosGerado.Add(boletoGerado);
                db.SaveChanges();
            }
            #endregion
             
            #region Cedente
            var c = new Cedente(empresa.CpfCnpj, empresa.Nome, conta.Agencia, conta.ContaCorrente.Split('-')[0], conta.ContaCorrente.Split('-')[1]);
            c.Codigo = conta.ContaCorrente;
            #endregion

            #region Boleto
            Boleto boleto = new Boleto(dataVencimento, valor, modeloBoleto.Carteira, boletoGerado.NossoNumero.ToString(), c, new EspecieDocumento(numeroBanco));
            boleto.NumeroDocumento = tituloRecorrenteId.ToString();
            boleto.Banco = new BoletoNet.Banco(numeroBanco);
            #endregion

            #region Instrucoes
            Instrucao item1 = new Instrucao(conta.Banco.Codigo);
            item1.Descricao = modeloBoleto.Instrucao;
            boleto.Instrucoes.Add(item1);
            #endregion

            #region Juros
            if (boletoGerado.Juros > 0)
            {
                Instrucao item2 = new Instrucao(conta.Banco.Codigo);
                boleto.JurosMora = Math.Round(boleto.ValorBoleto * boletoGerado.Juros / 100 / 30, 2);
                item2.Descricao = "Após o vencimento cobrar juros de R$ " + boleto.JurosMora + " ao dia";
                boleto.Instrucoes.Add(item2);
                if (dataVencimento < DateTime.Today && titulo.Categoria.DirecaoVencimentoDiaNaoUtil == DirecaoVencimento.Antecipado)
                {
                    boleto.DataVencimento = DateTime.Today;
                    int diasTotais = (int)(DateTime.Today - dataVencimento).TotalDays;
                    JurosTotais = boleto.JurosMora * diasTotais;
                }
                else if (dataVencimento < DateTime.Today && titulo.Categoria.DirecaoVencimentoDiaNaoUtil == DirecaoVencimento.Prorrogado)
                {
                    boleto.DataVencimento = DateTime.Today;
                    int diasTotais = (int)(DateTime.Today - dataVencimento).TotalDays;
                    JurosTotais = boleto.JurosMora * diasTotais;
                }
            }
            #endregion

            #region Multa
            if (boletoGerado.Multa > 0)
            {
                Instrucao item3 = new Instrucao(conta.Banco.Codigo);
                decimal multa = boleto.ValorBoleto * boletoGerado.Multa / 100;
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

            boleto.ValorBoleto += JurosTotais + boleto.ValorMulta;
            boleto.DataJurosMora = dataVencimento;
            boleto.DataMulta = dataVencimento;
            boleto.PercMulta = boletoGerado.Multa;
            boleto.PercJurosMora = boletoGerado.Juros;
            #endregion

            #region Desconto
            if (boletoGerado.PercentualDesconto > 0)
            {
                boleto.DataDesconto = dataVencimentoOriginal.AddDays(-boletoGerado.DiasDesconto);
                if (DateTime.Today <= boleto.DataDesconto)
                {
                    boleto.ValorDesconto = boleto.ValorBoleto * (boletoGerado.PercentualDesconto / 100);
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

        public static MemoryStream StringToPdf(string texto)
        {
            string apiKey = "06cbcf84-3b38-4070-bff1-cfff50cabbbf";
            using (var client = new WebClient())
            {
                NameValueCollection options = new NameValueCollection();
                options.Add("apikey", apiKey);
                options.Add("value", texto);
                options.Add("MarginLeft", "5");
                options.Add("MarginRight", "5");

                try
                {
                    return new MemoryStream(client.UploadValues("http://api.html2pdfrocket.com/pdf", options));
                }
                catch (Exception)
                {
                    throw new Exception("A API não está respondendo! Favor verificar");   
                }
            }

        }

        public static void EnviarBoletoPorEmail(int tituloId, string nomeArquivo, int modeloBoletoId, string emailDestinatario, string AssuntoEmail, string TextoEmail)
        {
            var titulo = new RepositorioRecebimento().BuscarPorId(tituloId);
            var emailRemetente = titulo.Empresa.EmailFinanceiro;

            var boletoGeradoId = new RepositorioBoletoGerado().Listar(b => b.TituloId == titulo.Id).FirstOrDefault().Id.ToString();
            TextoEmail = TextoEmail.Replace("#tokenBoleto#", EnderecoHost() + "/AtualizacaoBoleto?tokenBoleto=" + Criptografia.Encriptar(boletoGeradoId));

            var boleto = Util.GerarBoletoBancario(tituloId, modeloBoletoId);
            var pdf = StringToPdf(boleto.MontaHtmlEmbedded(false, true));
            var anexos = new List<Stream>();
            anexos.Add(pdf);
            var email = new Email(emailDestinatario, TextoEmail, AssuntoEmail, emailRemetente, anexos, new List<string>() { Path.GetFileName(nomeArquivo) });
            email.DispararMensagem();
        }


        public static void EnviarBoletoPorEmail(int tituloRecorrenteId, string nomeArquivo, int modeloBoletoId, decimal valor, DateTime dataVencimento, string emailDestinatario, string AssuntoEmail, string TextoEmail)
        {
            var titulo = new RepositorioTituloRecorrente().BuscarPorId(tituloRecorrenteId);
            var emailRemetente = titulo.Empresa.EmailFinanceiro;

            var boletoGeradoId = new RepositorioBoletoGerado().Listar(b => b.TituloRecorrenteId == titulo.Id && b.DataVencimento == dataVencimento).FirstOrDefault().Id.ToString();
            TextoEmail = TextoEmail.Replace("#tokenBoleto#", EnderecoHost() + "/AtualizacaoBoleto?tokenBoleto=" + Criptografia.Encriptar(boletoGeradoId));

            var boleto = Util.GerarBoletoBancario(tituloRecorrenteId, valor, dataVencimento, modeloBoletoId);
            var pdf = StringToPdf(boleto.MontaHtmlEmbedded(false, true));
            var anexos = new List<Stream>();
            anexos.Add(pdf);
            var email = new Email(emailDestinatario, TextoEmail, AssuntoEmail, emailRemetente, anexos, new List<string>() { Path.GetFileName(nomeArquivo) });
            email.DispararMensagem();
        }

          
        public static void EnviarBoletoPorEmail(LoteBoleto loteBoleto, string nomeArquivo)
        {
            string emailDestinatario = "";
            string emailRemetente = "";
            BoletoBancario boleto = null;
            var modeloBoleto = new RepositorioModeloBoleto().BuscarPorId(loteBoleto.ModeloBoletoId);
            string boletoGeradoId = null;

            if (loteBoleto.TituloId != null)
            {
                var titulo = new RepositorioRecebimento().BuscarPorId((int)loteBoleto.TituloId);
                emailDestinatario = loteBoleto.EmailDestinatario;
                emailRemetente = titulo.Empresa.EmailFinanceiro;
                boleto = Util.GerarBoletoBancario((int)loteBoleto.TituloId, loteBoleto.ModeloBoletoId);
                boletoGeradoId = new RepositorioBoletoGerado().Listar(b => b.TituloId == titulo.Id).FirstOrDefault().Id.ToString();
            }
            if (loteBoleto.TituloRecorrenteId != null)
            {
                var titulo = new RepositorioTituloRecorrente().BuscarPorId((int)loteBoleto.TituloRecorrenteId);
                emailDestinatario = loteBoleto.EmailDestinatario;
                emailRemetente = titulo.Empresa.EmailFinanceiro;
                boleto = Util.GerarBoletoBancario((int)loteBoleto.TituloRecorrenteId, loteBoleto.Valor, loteBoleto.DataVencimento, loteBoleto.ModeloBoletoId);
                boletoGeradoId = new RepositorioBoletoGerado().Listar(b => b.TituloRecorrenteId == titulo.Id && b.DataVencimento == loteBoleto.DataVencimento).FirstOrDefault().Id.ToString();
            }

            var pdf = StringToPdf(boleto.MontaHtmlEmbedded(false, true));
            var anexos = new List<Stream>();
            anexos.Add(pdf);

            modeloBoleto.TextoEmail = modeloBoleto.TextoEmail.Replace("#tokenBoleto#", EnderecoHost() + "/AtualizacaoBoleto?tokenBoleto=" + Criptografia.Encriptar(boletoGeradoId));

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
