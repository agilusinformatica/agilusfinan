using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class Convite : Padrao
    {
        public int Id { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Display(Name = "Perfil")]
        public int PerfilId { get; set; }
        public bool Expirado { get; set; }

        public virtual Perfil Perfil { get; set; }
    }
}
