using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class Perfil : Padrao
    {
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public virtual List<Acesso> Acessos { get; set; }
        public virtual List<Convite> Convites { get; set; }

        public Perfil()
        {
            Acessos = new List<Acesso>();
        }

    }
}
