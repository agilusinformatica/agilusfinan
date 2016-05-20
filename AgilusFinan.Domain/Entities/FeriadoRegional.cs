using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class FeriadoRegional : Padrao
    {
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
    }
}
