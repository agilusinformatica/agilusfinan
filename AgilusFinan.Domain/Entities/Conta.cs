using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int BancoId { get; set; }
        public string Agencia { get; set; }
        public string ContaCorrente { get; set; }
        public virtual Banco Banco { get; set; }
        public Boolean Padrao { get; set; }
        public virtual IList<ModeloBoleto> ModelosBoleto { get; set; }
    }
}