using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class ModeloBoleto : Padrao
    {
        public string Nome { get; set; }
        [Display(Name = "Conta")]
        public int ContaId { get; set; }
        public virtual Conta Conta { get; set; }
        public string Carteira { get; set; }
        [Display(Name = "Convênio")]
        public string Convenio { get; set; }
        [Display(Name = "Código do Cedente")]
        public string CodigoCedente { get; set; }
        public decimal Juros { get; set; }
        public decimal Multa { get; set; }
        [Display(Name = "Dias de Desconto")]
        public int DiasDesconto { get; set; }
        [Display(Name = "Percentual do Desconto")]
        public decimal PercentualDesconto { get; set; }
        [Display(Name = "Instrução")]
        [DataType(DataType.MultilineText)]
        public string Instrucao { get; set; }
        [Display(Name = "Nosso Número")]
        public int NossoNumero { get; set; }
        [Display(Name = "Assunto do E-mail")]
        public string AssuntoEmail { get; set; }
        [Display(Name = "Texto do E-mail")]
        public string TextoEmail { get; set; }
    }
}
