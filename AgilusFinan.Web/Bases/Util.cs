﻿using AgilusFinan.Domain.Entities;
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
        static private List<Categoria> itens;

        private static void AdicionaItem(Categoria c, int nivel)
        {
            string identador = System.Net.WebUtility.HtmlDecode("&nbsp;");
            lista.Add(c.Id, Repete(identador, nivel * 3) + c.Nome);
            var filhas = itens.Where(f => f.CategoriaPaiId == c.Id && f.Id != f.CategoriaPaiId);
            foreach (var item in filhas)
            {
                AdicionaItem(item, ++nivel);
                --nivel;
            }
        }

        public static Dictionary<int, string> CategoriasIdentadas(DirecaoCategoria? direcao)
        {
            var repo = new RepositorioCategoria();

            if (direcao != null)
            {
                itens = repo.Listar(c => c.Direcao == direcao);
            }
            else
            {
                itens = repo.Listar().ToList();
            }

            lista = new Dictionary<int, string>();
            var root = itens.Where(c => c.CategoriaPaiId == null);
            foreach (var item in root)
            {
                AdicionaItem(item, 0);
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
    }
}
