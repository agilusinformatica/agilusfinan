using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class PrevistoRealizado
    {
        public string Tipo { get; set; }
        public decimal Previsto { get; set; }
        public decimal Realizado { get; set; }
        public decimal Porcentagem
        {
            get
            {
                return Previsto > 0 ? Realizado / Previsto * 100 : 0;
            }
        }
    }

}
