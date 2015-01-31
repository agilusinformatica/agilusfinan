namespace AgilusFinan.Domain.Entities
{
    public class TelefonePessoa
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public Telefone Telefone { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}
