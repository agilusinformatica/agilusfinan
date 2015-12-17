using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class LoteBoleto : ConsultaPadrao
    {
        public int? TituloRecorrenteId { get; set; }
        public int? TituloId { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public string NomePessoa { get; set; }
        public bool Email { get; set; }
        public bool Boleto { get; set; }
        public int ModeloBoletoId { get; set; }
    }
}
