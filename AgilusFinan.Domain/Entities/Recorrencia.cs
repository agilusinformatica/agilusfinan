using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class Recorrencia
    {
        public PeriodoRecorrencia Periodo { get; set; }
        public int QuantidadeParcelas { get; set; }
    }

    public enum PeriodoRecorrencia {Nenhum, Semanal, Quinzenal, Mensal, Bimestral, Trimestral, Semestral, Anual }
}
