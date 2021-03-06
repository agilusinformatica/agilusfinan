﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class Conta : Padrao
    {
        public string Nome { get; set; }
        [Display(Name="Saldo Inicial")]
        public decimal SaldoInicial { get; set; }
        [Display(Name = "Data do Saldo Inicial")]
        public DateTime DataSaldoInicial { get; set; }
        [Display(Name = "Banco")]
        public int BancoId { get; set; }
        [Display(Name = "Agência")]
        public string Agencia { get; set; }
        [Display(Name = "Conta Corrente")]
        public string ContaCorrente { get; set; }
        [JsonIgnore]
        public virtual Banco Banco { get; set; }
        [Display(Name = "Principal?")]
        public Boolean Padrao { get; set; }
        [JsonIgnore]
        public virtual IList<ModeloBoleto> ModelosBoleto { get; set; }
    }
}