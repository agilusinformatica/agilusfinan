using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class Conta : Padrao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal SaldoInicial { get; set; }
        public DateTime DataSaldoInicial { get; set; }
        public int BancoBoletoId { get; set; }
        public string Agencia { get; set; }
        public string ContaCorrente { get; set; }
        public string Carteira { get; set; }
        public string Convenio { get; set; }
        public string CodigoCedente { get; set; }
        public virtual Banco BancoBoleto { get; set; }
        public Boolean Padrao { get; set; }
    }
}