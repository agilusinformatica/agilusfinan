using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class TituloPendente : Padrao
    {
        public int? tituloRecorrenteId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public int CategoriaId { get; set; }
        public int PessoaId { get; set; }
        public int? CentroCustoId { get; set; }
    }
}
