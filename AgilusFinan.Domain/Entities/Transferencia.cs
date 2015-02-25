using System;

namespace AgilusFinan.Domain.Entities
{
    public class Transferencia
    {
        public int Id { get; set; }
        public int ContaOrigemId { get; set; }
        public virtual Conta ContaOrigem { get; set; }
        public int ContaDestinoId { get; set; }
        public virtual Conta ContaDestino { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
          
    }
}
