using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class ResumoTituloCategoria
    {
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public int? CategoriaPaiId { get; set; }
        public int Cor { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        [Display(Name = "Valor Previsto")]
        public decimal? ValorPrevisto { get; set; }
        [Display(Name = "Valor Ralizado")]
        public decimal? ValorRealizado { get; set; }


    }
}
