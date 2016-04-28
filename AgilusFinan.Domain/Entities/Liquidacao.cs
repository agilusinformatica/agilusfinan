using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class Liquidacao
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        [Display(Name = "Juros e Multa")]
        public decimal? JurosMulta { get; set; }
        [Display(Name = "Forma de Liquidação")]
        public FormaLiquidacao? FormaLiquidacao { get; set; }
        public decimal? Desconto { get; set; }
        public int TituloId { get; set; }
        public virtual Titulo Titulo { get; set; }
        public string ConciliacaoExtratoId { get; set; }

    }
}
