using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class Usuario : Padrao
    {
        public string Nome { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        public int PerfilId { get; set; }
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        [Display(Name = "Endereço")]
        public Endereco Endereco { get; set; }
        public virtual Perfil Perfil { get; set; }

        public Usuario()
        {
            Endereco = new Endereco();
        }
    }
}
