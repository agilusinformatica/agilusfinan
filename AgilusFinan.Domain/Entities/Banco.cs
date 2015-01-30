namespace AgilusFinan.Domain.Entities
{
    public class Banco
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public int EmpresaId { get; set; }

        public virtual Empresa Empresa { get; set; }
    }
}
