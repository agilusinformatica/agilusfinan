using System.Collections.Generic;

namespace AgilusFinan.Domain.Entities
{
    public class TipoPessoa : Padrao
    {
        public string Nome { get; set; }

        public virtual IEnumerable<TipoPessoaPorPessoa> Pessoas { get; set; }
    }
}
