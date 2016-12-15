using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class Extrato : ConsultaPadrao
    {
        public DateTime Data { get; set; }
        public Decimal? Valor { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public Decimal Saldo { get; set; }
        public string Categoria { get; set; }
        public DateTime? DataVencimento { get; set; }

    }
}
