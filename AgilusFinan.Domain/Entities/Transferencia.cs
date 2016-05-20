using System;
using System.ComponentModel.DataAnnotations;

namespace AgilusFinan.Domain.Entities
{
    public class Transferencia : Padrao
    {
        [Display(Name = "Conta Origem")]
        public int ContaOrigemId { get; set; }
        public virtual Conta ContaOrigem { get; set; }
        [Display(Name = "Conta Destino")]
        public int ContaDestinoId { get; set; }
        public virtual Conta ContaDestino { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
          
    }
}
