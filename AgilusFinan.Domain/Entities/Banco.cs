using System.ComponentModel.DataAnnotations;

namespace AgilusFinan.Domain.Entities
{
    public class Banco : Padrao
    {
        public int Id { get; set; }
        [Display(Name="Código")]
        public int Codigo { get; set; }
        public string Nome { get; set; }
    }
}
