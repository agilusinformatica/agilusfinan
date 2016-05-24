using Newtonsoft.Json;
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
        [JsonIgnore]
        public virtual List<Acesso> Acessos { get; set; }
        [JsonIgnore]
        public virtual List<Convite> Convites { get; set; }

        public Perfil()
        {
            Acessos = new List<Acesso>();
        }

    }
}
