using System;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgilusFinan.Web.ViewModels
{
    public class TituloViewModel
    {
        public int Id { get; set; }
        public int ContaId { get; set; }
        public Conta Conta { get; set; }
        public DateTime DataVencimento { get; set; }
        public String Descricao { get; set; }
        public decimal Valor { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
        public int? CentroCustoId { get; set; }
        public CentroCusto CentroCusto { get; set; }
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Competencia { get; set; }
        public string Observacao { get; set; }
        
        public List<Liquidado> Liquidacoes { get; set; }

        public TituloViewModel()
        {
            Conta = new Conta();
            Categoria = new Categoria();
            Pessoa = new Pessoa();
            CentroCusto = new CentroCusto();
            Liquidacoes = new List<Liquidado>(); 

            foreach (var liquidacao in new RepositorioLiquidacao().Listar())
            {
                Liquidacoes.Add(new Liquidado()
                {
                    Id = liquidacao.Id,
                    Data = liquidacao.Data,
                    Valor = liquidacao.Valor,
                    JurosMulta = liquidacao.JurosMulta,
                    FormaLiquidacao = liquidacao.FormaLiquidacao,
                    TituloId = liquidacao.TituloId
                });
            }
        }
    }

    public class Liquidado
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public decimal JurosMulta { get; set; }
        public FormaLiquidacao FormaLiquidacao { get; set; }
        public int TituloId { get; set; }
    }
}