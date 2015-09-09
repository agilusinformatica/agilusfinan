using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class Acesso
    {
        public int Id { get; set; }
        public int FuncaoId { get; set; }
        public int PerfilId { get; set; }
        public Funcao Funcao { get; set; }
        public Perfil Perfil { get; set; }
    }
}
