using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class TituloRecorrente : Padrao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int DiaVencimento { get; set; }
        public decimal? Valor { get; set; }
        public PeriodoRecorrencia Recorrencia { get; set; }
        public int? QtdeParcelas { get; set; }
        public bool Ativo { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual Categoria Categoria { set; get; }
        public virtual CentroCusto CentroCusto { get; set; }
        public int PessoaId { get; set; }
        public int CategoriaId { get; set; }
        public int? CentroCustoId { get; set; }
        public virtual DateTime DataCadastro { get { return DateTime.Now.Date; } set {} }

        public virtual IList<Titulo> Titulos { get; set; }
    }

    public enum PeriodoRecorrencia { Semanal, Quinzenal, Mensal, Bimestral, Trimestral, Semestral, Anual }
}
