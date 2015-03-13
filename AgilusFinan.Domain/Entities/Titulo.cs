using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class Titulo : Padrao
    {
        public int Id { get; set; }
        public int ContaId { get; set; }
        public virtual Conta Conta { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorPago { get; set; }
        public String Descricao { get; set; }
        public decimal Valor { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
        public int PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public int CentroCustoId { get; set; }
        public virtual CentroCusto CentroCusto { get; set; }
        public DateTime Competencia { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        public decimal JurosMulta { get; set; }
        public String Observacao { get; set; }
        public Recorrencia Recorrencia { get; set; }
    }
}
