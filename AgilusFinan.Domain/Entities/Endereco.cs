using System.ComponentModel.DataAnnotations;
namespace AgilusFinan.Domain.Entities
{
    public class Endereco
    {
        [Display(Name="")]
        public string Logradouro { get; set; }
        [Display(Name = "Número")]
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        [Display(Name = "UF")]
        public string Uf { get; set; }
        [Display(Name = "CEP")]
        public string Cep { get; set; }
    }
}
