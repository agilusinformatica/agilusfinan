using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using AgilusFinan.Infra.Services;

namespace AgilusFinan.Web.Bases
{
    public static class Cache
    {
        public static object Busca(string nome, Dictionary<string, string> parametros)
        {
            if (ConsultaCache.Existe(nome, parametros))
            {
                return HttpContext.Current.Cache[nomeCompleto(nome, parametros)];
            }
            return null;        
        }

        public static void Insere(string nome, Dictionary<string, string> parametros, object valor)
        {
            ConsultaCache.Gravar(nome, parametros);
            HttpContext.Current.Cache.Insert(nomeCompleto(nome, parametros), valor);
        }

        private static string nomeCompleto(string nome, Dictionary<string, string> parametros)
        {
            string nomeCompleto = nome;
            foreach (var item in parametros)
            {
                nomeCompleto += item.Value;
            }
            return nomeCompleto;
        }

    }
}
