using System;

namespace AgilusFinan.Domain.Entities
{
    public class Recebimento : Padrao
    {
        public int Id { get; set; }
        public int ContaId { get; set; }
        public virtual Conta Conta { get; set; }
        public DateTime DataVencimento { get; set; }
        public String Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataRecebimento { get; set; }
        public decimal ValorRecebido { get; set; }
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
