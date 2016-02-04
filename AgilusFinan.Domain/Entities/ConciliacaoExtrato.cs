using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class ConciliacaoExtrato
    {
        //Objeto que encapsula as informações contidas em <STMTTRN> do arquivo OFX
        public string Id { get; set; }
        public TipoLancamento TipoLancamento { get; set; }
        public DateTime DataLancamento { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
    }
    public enum TipoLancamento { Debito, Credito }
}
