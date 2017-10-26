using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class FaturaGerada : Padrao
    {

        public string IuguId { get; set; }
        [Display(Name = "Data Vencimento")]
        public DateTime DataVencimento { get; set; }
        public string UrlFatura { get; set; }
        [Display(Name = "Título")]
        public int? TituloId { get; set; }
        [Display(Name = "Título Recorrente")]
        public int? TituloRecorrenteId { get; set; }

        public virtual TituloRecorrente TituloRecorrente { get; set; }
        public virtual Titulo Titulo { get; set; }
    }
}
