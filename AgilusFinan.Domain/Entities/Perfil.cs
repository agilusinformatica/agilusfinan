using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class Perfil : Padrao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public virtual IEnumerable<Acesso> Acesso { get; set; }
    }
}
