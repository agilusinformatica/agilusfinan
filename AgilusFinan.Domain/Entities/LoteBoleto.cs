using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class LoteBoleto : ConsultaPadrao
    {
        public int? TituloRecorrenteId { get; set; }
        public int? TituloId { get; set; }
        [Display(Name = "Data de Vencimento")]
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
        [Display(Name = "Nome da Pessoa")]
        public string NomePessoa { get; set; }
        public bool Boleto { get; set; }
        public int ModeloBoletoId { get; set; }
        [Display(Name = "E-mail Destinatário")]
        public string EmailDestinatario { get; set; }
    }
}
