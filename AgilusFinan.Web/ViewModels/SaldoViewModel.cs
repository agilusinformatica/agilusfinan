using AgilusFinan.Domain.Entities;
using AgilusFinan.Web.Bases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgilusFinan.Web.ViewModels
{
    public class SaldoViewModel
    {
        public Conta Conta { get; set; }
        public decimal Saldo { get; set; }
    }
}