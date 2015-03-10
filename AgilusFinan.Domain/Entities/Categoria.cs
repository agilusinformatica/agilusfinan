using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgilusFinan.Domain.Entities
{
    public class Categoria : Padrao
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [Display(Name = "Direção")]
        public DirecaoCategoria Direcao { get; set; }
        public int Cor { get; set; }

        [Display(Name = "Categoria Pai")]
        public int CategoriaPaiId { get; set; }
        public virtual Categoria CategoriaPai { get; set; }
        public virtual IEnumerable<Categoria> CategoriasFilhas { get; set; } 
    }

    public enum DirecaoCategoria { Recebimento, Pagamento }
}
