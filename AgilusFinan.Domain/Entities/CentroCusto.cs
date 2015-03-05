using System.ComponentModel;

namespace AgilusFinan.Domain.Entities
{
    public class CentroCusto
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}
