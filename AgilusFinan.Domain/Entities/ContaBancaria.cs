namespace AgilusFinan.Domain.Entities
{
    public class ContaBancaria
    {
        public int BancoId { get; set; }
        public string Agencia { get; set; }
        public string Numero { get; set; }
        public bool Poupanca { get; set; }
        public string NomeTitular { get; set; }
        public string CpfTitular { get; set; }
    }
}
