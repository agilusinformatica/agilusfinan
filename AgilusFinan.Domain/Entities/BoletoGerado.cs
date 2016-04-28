using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class BoletoGerado : Padrao
    {
        public int Id { get; set; }
        [Display(Name = "Titulo")]
        public int? TituloId { get; set; }
        [Display(Name = "Titulo Recorrente")]
        public int? TituloRecorrenteId { get; set; }
        [Display(Name = "Modelo de Boleto")]
        public int ModeloBoletoId { get; set; }
        [Display(Name = "Nosso Número")]
        public int NossoNumero { get; set; }
        [Display(Name = "Data de Vencimento")]
        public DateTime? DataVencimento { get; set; }

    }
}
