using System.Collections.Generic;

namespace AgilusFinan.Domain.Entities
{
    public class Pessoa
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public Endereco Endereco { get; set; }
        public ContaBancaria ContaBancaria { get; set; }
        public virtual IEnumerable<TelefonePessoa> Telefones { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual IEnumerable<TipoPessoaPorPessoa> TiposPessoa { get; set; }

    }
}
