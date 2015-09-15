using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Utils;
using AgilusFinan.Infra.Context;
using AgilusFinan.Infra.Services;
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

        private static void AdicionaItemCategoria(Categoria c, int nivel)
        {
            string identador = System.Net.WebUtility.HtmlDecode("&nbsp;");
            lista.Add(c.Id, Repete(identador, nivel * 3) + c.Nome);
            var filhas = itensCategoria.Where(f => f.CategoriaPaiId == c.Id && f.Id != f.CategoriaPaiId);
            foreach (var item in filhas)
            {
                AdicionaItemCategoria(item, ++nivel);
                --nivel;
            }
        }

        public static Dictionary<int, string> CategoriasIdentadas(DirecaoCategoria? direcao)
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
                AdicionaItemCategoria(item, 0);
            }

            return lista;
        }

        private static void AdicionaItemFuncao(Funcao c, int nivel)
        {
            string identador = System.Net.WebUtility.HtmlDecode("&nbsp;");
            lista.Add(c.Id, Repete(identador, nivel * 3) + c.Descricao);
            var filhas = itensFuncao.Where(f => f.FuncaoSuperiorId == c.Id && f.Id != f.FuncaoSuperiorId);
            foreach (var item in filhas)
            {
                AdicionaItemFuncao(item, ++nivel);
                --nivel;
            }
        }

        public static Dictionary<int, string> FuncoesIdentadas()
        {
            itensFuncao = new Contexto().Funcoes.ToList();

            lista = new Dictionary<int, string>();
            var root = itensFuncao.Where(c => c.FuncaoSuperiorId == null);
            foreach (var item in root)
            {
                AdicionaItemFuncao(item, 0);
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
            string host = 
                System.Web.HttpContext.Current.Request.Url.Scheme+@"://"+
                System.Web.HttpContext.Current.Request.Url.Host+":"+
                System.Web.HttpContext.Current.Request.Url.Port.ToString();

            string token = Criptografia.Encriptar(convite.Email + "|" + convite.PerfilId.ToString() + "|" + empresaId.ToString());
            var Email = new Email(convite.Email, host+"/Login/EfetivarConvite?token=" + token, "Convite", remetente);
            Email.DispararMensagem();
        }
    }
}
