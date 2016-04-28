using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class FluxoCaixa : ConsultaPadrao
    {
        [Display(Name = "Período")]
        public String Periodo { get; set; }
        [Display(Name = "Saldo Inicial")]
        public Decimal SaldoInicial { get; set; }
        public Decimal Receitas { get; set; }
        public Decimal Despesas { get; set; }
        [Display(Name = "Lucro / Prejuizo")]
        public Decimal LucroPrejuizo { get; set; }
        public Decimal Acumulado { get; set; }
        public Double Lucratividade { get; set; }

    }
    public enum Periodicidade
    {
        Mensal, Bimestral, Trimestral, Semestral, Anual
    }
}
