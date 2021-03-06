﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class Filtro
    {
        public List<ParametroFiltro> Parametros { get; set; }
        public Filtro()
        {
            Parametros = new List<ParametroFiltro>();
        }

        public dynamic ValorPorNome(string nome)
        {
            var parametro = Parametros.Find(p => p.Nome == nome);
            return parametro.Valor;
        }

        public void AdicionaParametro(string nome, dynamic valor, TipoFiltro tipo)
        {
            this.Parametros.Add(new ParametroFiltro()
                {
                    Nome = nome,
                    Tipo = tipo,
                    Valor = valor
                });
        }
    }

    public enum TipoFiltro
    {
        texto, valor, monetario, data, logico, conta, centrocusto, categoria, pessoa, tipopessoa, tipotelefone, usuario, perfil, banco, modeloboleto, periodicidade
    }

    public class ParametroFiltro
    {
        public string Nome { get; set; }
        public string Label { get; set; }
        public TipoFiltro Tipo { get; set; }
        public dynamic Valor { get; set; }
    }

}
