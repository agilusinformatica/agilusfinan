using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class ConciliacaoExtrato
    {
        //Objeto que encapsula as informações contidas em <STMTTRN> do arquivo OFX
        public int? Id { get; set; }
        public tipoLancamento TipoLancamento { get; set; }
        public DateTime DataLancamento { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
    }
    public enum tipoLancamento { Debito, Credito }
}
