using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class ConciliacaoExtrato
    {
        //Objeto que encapsula as informações contidas em <STMTTRN> do arquivo OFX
        public string Id { get; set; }
        [Display(Name = "Tipo de Lançamento")]
        public TipoLancamento TipoLancamento { get; set; }
        [Display(Name = "Data de Lançamento")]
        public DateTime DataLancamento { get; set; }
        public decimal Valor { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
    public enum TipoLancamento { Debito, Credito }
}
