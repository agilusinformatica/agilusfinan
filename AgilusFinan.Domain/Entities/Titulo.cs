using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class Titulo : Padrao
    {
        public int ContaId { get; set; }
        public virtual Conta Conta { get; set; }
        public DateTime DataVencimento { get; set; }
        public String Descricao { get; set; }
        public decimal Valor { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
        public int PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public int? CentroCustoId { get; set; }
        public virtual CentroCusto CentroCusto { get; set; }
        public DateTime? Competencia { get; set; }
        public string Observacao { get; set; }
        public int? TituloRecorrenteId { get; set; }
        public virtual TituloRecorrente TituloRecorrente { get; set; }
        public virtual IList<Liquidacao> Liquidacoes { get; set; }
        public string Liquidado 
        {
            get
            {
                decimal soma = Liquidacoes.Sum(d => d.Valor);

                if (soma >= Valor && Valor > 0) 
                    return "Pago";
                else if (soma > 0)
                    return "Parcialmente";
                else
                    return "Não Pago";
            }
        }


        public Titulo()
        {
            Liquidacoes = new List<Liquidacao>();
        }

    }
}
