﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class TituloPendente
    {
        public int? TituloRecorrenteId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal? Valor { get; set; }
        public int CategoriaId { get; set; }
        public int ContaId { get; set; }
        public int PessoaId { get; set; }
        public int? CentroCustoId { get; set; }
        public int? TituloId { get; set; }
        public string NomeCategoria { get; set; }
        public string NomePessoa { get; set; }
        public string NomeCentroCusto { get; set; }
        public DirecaoCategoria Direcao { get; set; }
        public string NomeConta { get; set; }
    }
}
