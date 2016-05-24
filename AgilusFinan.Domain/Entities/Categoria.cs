using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgilusFinan.Domain.Entities
{
    public class Categoria : Padrao
    {
        public string Nome { get; set; }

        [Display(Name = "Direção")]
        public DirecaoCategoria Direcao { get; set; }
        [DataType("Color")]
        public int Cor { get; set; }
        [Display(Name = "Categoria Pai")]
        public int? CategoriaPaiId { get; set; }
        [JsonIgnore]
        public virtual Categoria CategoriaPai { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Categoria> CategoriasFilhas { get; set; }
        [Display(Name = "Vencimento em dias não úteis")]
        public DirecaoVencimento DirecaoVencimentoDiaNaoUtil { get; set; }

    }

    public enum DirecaoCategoria { Recebimento, Pagamento }
    public enum DirecaoVencimento { Antecipado, Prorrogado }
}
