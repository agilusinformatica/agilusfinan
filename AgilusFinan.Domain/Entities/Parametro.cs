using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AgilusFinan.Domain.Entities
{
    public class Parametro : Padrao
    {
        [Display(Name = "Texto do E-mail")]
        public string TextoEmailLiquidacao { get; set; }
        [Display(Name = "Assunto do E-mail")]
        public string AssuntoEmailLiquidacao { get; set; }
        public string TokenIUGU { get; set; }
    }
}
