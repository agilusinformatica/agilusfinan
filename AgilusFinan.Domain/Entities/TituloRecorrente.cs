using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class TituloRecorrente : Padrao
    {
        public string Nome { get; set; }
        [Display(Name = "Dia de Vencimento")]
        public int DiaVencimento { get; set; }
        public decimal? Valor { get; set; }
        [Display(Name = "Conta")]
        public int ContaId { get; set; }
        [JsonIgnore]
        public virtual Conta Conta { get; set; }
        [Display(Name = "Recorrência")]
        public PeriodoRecorrencia Recorrencia { get; set; }
        [Display(Name = "Quantidade de Parcelas")]
        public int? QtdeParcelas { get; set; }
        [Display(Name = "Ativo?")]
        public bool Ativo { get; set; }
        [JsonIgnore]
        public virtual Pessoa Pessoa { get; set; }
        [JsonIgnore]
        public virtual Categoria Categoria { set; get; }
        [JsonIgnore]
        public virtual CentroCusto CentroCusto { get; set; }
        [Display(Name = "Pessoa")]
        public int PessoaId { get; set; }
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
        [Display(Name = "Centro de Custo")]
        public int? CentroCustoId { get; set; }
        [Display(Name="A partir de")]
        public DateTime DataCadastro { get; set; }
        [JsonIgnore]
        public virtual IList<Titulo> Titulos { get; set; }
    }

    public enum PeriodoRecorrencia { Semanal, Quinzenal, Mensal, Bimestral, Trimestral, Semestral, Anual }
}
