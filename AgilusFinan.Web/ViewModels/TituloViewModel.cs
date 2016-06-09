using System;
using AgilusFinan.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using AgilusFinan.Web.Bases.Interfaces;

namespace AgilusFinan.Web.ViewModels
{
    public class TituloViewModel : ViewModel<Titulo>
    {
        public int Id { get; set; }
        [Display(Name = "Conta")]
        public int ContaId { get; set; }
        [JsonIgnore]
        public Conta Conta { get; set; }
        [Display(Name = "Data de Vencimento")]
        public DateTime DataVencimento { get; set; }
        [Display(Name = "Descrição")]
        public String Descricao { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? Valor { get; set; }
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
        [JsonIgnore]
        public Categoria Categoria { get; set; }
        [Display(Name = "Pessoa")]
        public int PessoaId { get; set; }
        [JsonIgnore]
        public Pessoa Pessoa { get; set; }
        [Display(Name = "Centro de Custo")]
        public int? CentroCustoId { get; set; }
        [JsonIgnore]
        public CentroCusto CentroCusto { get; set; }
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Competência")]
        public DateTime? Competencia { get; set; }
        [Display(Name = "Observação")]
        public string Observacao { get; set; }
        public int? TituloRecorrenteId { get; set; }
        public string TipoTitulo { get; set; }
        public List<LiquidacaoViewModel> Liquidacoes { get; set; }

        public TituloViewModel()
        {
            Conta = new Conta();
            Categoria = new Categoria();
            Pessoa = new Pessoa();
            CentroCusto = new CentroCusto();
            Liquidacoes = new List<LiquidacaoViewModel>(); 
        }


        public void FromModel(Titulo model)
        {
            this.Id = model.Id;
            this.CategoriaId = model.CategoriaId;
            this.CentroCustoId = model.CentroCustoId;
            this.PessoaId = model.PessoaId;
            this.Competencia = model.Competencia;
            this.ContaId = model.ContaId;
            this.DataVencimento = model.DataVencimento;
            this.Descricao = model.Descricao;
            this.Valor = model.Valor;
            this.Observacao = model.Observacao;
            this.TituloRecorrenteId = model.TituloRecorrenteId;
            this.TipoTitulo = model.ToString().Split('.')[1]; //Isto foi feito para identificar se o título é pagamento ou um recebimento.
            
            foreach (var l in model.Liquidacoes)
            {
                this.Liquidacoes.Add(new LiquidacaoViewModel()
                {
                    Id = l.Id,
                    Data = l.Data,
                    Valor = l.Valor,
                    JurosMulta = l.JurosMulta,
                    FormaLiquidacao = l.FormaLiquidacao,
                    Desconto = l.Desconto
                });
            }
        }

        public Titulo ToModel()
        {
            Titulo titulo = new Titulo();
            titulo.Id = this.Id;
            titulo.CategoriaId = this.CategoriaId;
            titulo.CentroCustoId = this.CentroCustoId;
            titulo.PessoaId = this.PessoaId;
            titulo.Competencia = this.Competencia;
            titulo.ContaId = this.ContaId;
            titulo.DataVencimento = this.DataVencimento;
            titulo.Descricao = this.Descricao;
            titulo.Valor = this.Valor > 0 ? (decimal)this.Valor : 0;
            titulo.Observacao = this.Observacao;
            titulo.TituloRecorrenteId = this.TituloRecorrenteId;
            
            foreach (var l in this.Liquidacoes)
            {
                titulo.Liquidacoes.Add(new Liquidacao()
                {
                    Data = l.Data,
                    Valor = l.Valor,
                    JurosMulta = l.JurosMulta,
                    FormaLiquidacao = l.FormaLiquidacao,
                    TituloId = this.Id,
                    Desconto = l.Desconto
                });
            }
            return titulo;
        }
    }

    public class LiquidacaoViewModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public decimal? JurosMulta { get; set; }
        public decimal? Desconto { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public FormaLiquidacao? FormaLiquidacao { get; set; }
    }
}