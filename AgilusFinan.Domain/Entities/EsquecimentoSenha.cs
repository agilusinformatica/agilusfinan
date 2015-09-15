using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class EsquecimentoSenha : Padrao
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public bool Expirado { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
