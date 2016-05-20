using System.ComponentModel.DataAnnotations;

namespace AgilusFinan.Domain.Entities
{
    public class Banco : Padrao
    {
        [Display(Name="Código")]
        public int Codigo { get; set; }
        [StringLength(100, ErrorMessage = "Tamanho máximo do campo de 100 caracteres")]
        public string Nome { get; set; }
    }
}
