using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgilusFinan.Domain.Entities
{
    public class ContaBancaria
    {
        [Display(Name = "Banco")]
        public int? BancoId { get; set; }
        [Display(Name = "Agência")]
        public string Agencia { get; set; }
        [Display(Name = "Conta Corrente")]
        public string Numero { get; set; }
        [Display(Name = "Poupança?")]
        public bool Poupanca { get; set; }
        [Display(Name = "Nome do Titular")]
        public string NomeTitular { get; set; }
        [Display(Name = "CPF do Titular")]
        public string CpfTitular { get; set; }
    }
}
