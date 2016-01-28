using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class Liquidacao
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public decimal JurosMulta { get; set; }
        public FormaLiquidacao? FormaLiquidacao { get; set; }
        public decimal Desconto { get; set; }
        public int TituloId { get; set; }
        public virtual Titulo Titulo { get; set; }
        public int? ConciliacaoExtratoId { get; set; }

    }
}
