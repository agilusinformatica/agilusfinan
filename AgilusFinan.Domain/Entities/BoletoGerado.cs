using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class BoletoGerado : Padrao
    {
        public int Id { get; set; }
        public int? TituloId { get; set; }
        public int? TituloRecorrenteId { get; set; }
        public int ModeloBoletoId { get; set; }
        public int NossoNumero { get; set; }
        public DateTime? DataVencimento { get; set; }

    }
}
