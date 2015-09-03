using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class Convite : Padrao
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int PerfilId { get; set; }
        public bool Expirado { get; set; }

        public virtual Perfil Perfil { get; set; }
    }
}
