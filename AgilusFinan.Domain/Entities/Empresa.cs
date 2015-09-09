namespace AgilusFinan.Domain.Entities
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string EmailContato { get; set; }
        public string EmailFinanceiro { get; set; }
        public byte[] Logotipo { get; set; }
        public bool Ativo { get; set; }
        public Endereco Endereco { get; set; }
    }
}
