﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class Usuario : Padrao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int PerfilId { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        public Endereco Endereco { get; set; }
        public virtual Perfil Perfil { get; set; }

        public Usuario()
        {
            Endereco = new Endereco();
        }
    }
}
