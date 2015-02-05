namespace AgilusFinan.Domain.Entities
{
    public class TipoPessoaPorPessoa
    {
        public int Id { get; set; }
        public int TipoPessoaId { get; set; }
        public int PessoaId { get; set; }

        public virtual TipoPessoa TipoPessoa { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}
