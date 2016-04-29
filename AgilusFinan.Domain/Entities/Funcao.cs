using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class Funcao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Endereco { get; set; }
        public int? FuncaoSuperiorId { get; set; }
        public IEnumerable<Acesso> Acessos { get; set; }
        public virtual Funcao FuncaoSuperior { get; set; }
        public virtual IEnumerable<Funcao> FuncoesFilhas { get; set; }
    }
}
