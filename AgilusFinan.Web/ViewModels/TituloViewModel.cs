using System;
using AgilusFinan.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AgilusFinan.Web.ViewModels
{
    public class TituloViewModel
    {
        public int Id { get; set; }
        public int ContaId { get; set; }
        public Conta Conta { get; set; }
        [Display(Name = "Data de Vencimento")]
        public DateTime DataVencimento { get; set; }
        public String Descricao { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? Valor { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
        public int? CentroCustoId { get; set; }
        public CentroCusto CentroCusto { get; set; }
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Competencia { get; set; }
        public string Observacao { get; set; }
        public int? TituloRecorrenteId { get; set; }
        
        public List<LiquidacaoViewModel> Liquidacoes { get; set; }

        public TituloViewModel()
        {
            Conta = new Conta();
            Categoria = new Categoria();
            Pessoa = new Pessoa();
            CentroCusto = new CentroCusto();
            Liquidacoes = new List<LiquidacaoViewModel>(); 

        }
    }

    public class LiquidacaoViewModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public decimal JurosMulta { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FormaLiquidacao? FormaLiquidacao { get; set; }
    }
}