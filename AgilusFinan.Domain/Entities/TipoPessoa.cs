using System.Collections.Generic;

namespace AgilusFinan.Domain.Entities
{
    public class TipoPessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int EmpresaId { get; set; }

        public virtual IEnumerable<TipoPessoaPorPessoa> Pessoas { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}
