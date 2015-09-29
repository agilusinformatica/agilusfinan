using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class Extrato
    {
        public DateTime Data { get; set; }
        public Decimal Valor { get; set; }
        public string Descricao { get; set; }
        public Decimal Saldo { get; set; }
    }
}
