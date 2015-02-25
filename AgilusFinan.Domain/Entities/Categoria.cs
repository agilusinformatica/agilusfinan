using System.Collections.Generic;

namespace AgilusFinan.Domain.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DirecaoCategoria Direcao { get; set; }
        public int Cor { get; set; }
        public int CategoriaPaiId { get; set; }

        public virtual Categoria CategoriaPai { get; set; }
        public virtual IEnumerable<Categoria> CategoriasFilhas { get; set; } 
    }

    public enum DirecaoCategoria { Recebimento, Pagamento }
}
