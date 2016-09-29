using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class BoletoGerado : Padrao
    {
        [Display(Name = "Titulo")]
        public int? TituloId { get; set; }
        [Display(Name = "Titulo Recorrente")]
        public int? TituloRecorrenteId { get; set; }
        [Display(Name = "Modelo de Boleto")]
        public int ModeloBoletoId { get; set; }
        [Display(Name = "Nosso Número")]
        public int NossoNumero { get; set; }
        [Display(Name = "Data de Vencimento")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}")]
        public DateTime? DataVencimento { get; set; }
        public decimal PercentualDesconto { get; set; }
        public decimal Juros { get; set; }
        public decimal Multa { get; set; }
        [Display(Name = "Dias de Desconto")]
        public int DiasDesconto { get; set; }

        public virtual TituloRecorrente TituloRecorrente { get; set; }
        public virtual Titulo Titulo { get; set; }
        public virtual ModeloBoleto ModeloBoleto { get; set; }

    }
}
