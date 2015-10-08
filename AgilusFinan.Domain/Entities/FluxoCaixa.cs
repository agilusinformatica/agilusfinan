using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class FluxoCaixa : ConsultaPadrao
    {
        public String Periodo { get; set; }
        public Decimal SaldoInicial { get; set; }
        public Decimal Receitas { get; set; }
        public Decimal Despesas { get; set; }
        public Decimal LucroPrejuizo { get; set; }
        public Decimal Acumulado { get; set; }
        public Double Lucratividade { get; set; }

    }
}
