using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class ModeloBoleto : Padrao
    {
        public int Id { get; set; }
        public int ContaId { get; set; }
        public virtual Conta Conta { get; set; }
        public string Carteira { get; set; }
        public string Convenio { get; set; }
        public string CodigoCedente { get; set; }
        public decimal Juros { get; set; }
        public decimal Multa { get; set; }
        public string Instrucao { get; set; }
        public int NossoNumero { get; set; }
    }
}
