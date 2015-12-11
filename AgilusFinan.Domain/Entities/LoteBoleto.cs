using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class LoteBoleto : ConsultaPadrao
    {
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public int PessoaId { get; set; }
        public bool Email { get; set; }
        public bool Boleto { get; set; }
    }
}
