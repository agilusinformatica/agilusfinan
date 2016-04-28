using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class TituloPendente
    {
        public int? TituloRecorrenteId { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Data de Vencimento")]
        public DateTime DataVencimento { get; set; }
        public decimal? Valor { get; set; }
        public int CategoriaId { get; set; }
        public int ContaId { get; set; }
        public int PessoaId { get; set; }
        public int? CentroCustoId { get; set; }
        public int? TituloId { get; set; }
        [Display(Name = "Categoria")]
        public string NomeCategoria { get; set; }
        [Display(Name = "Pessoa")]
        public string NomePessoa { get; set; }
        [Display(Name = "Centro de Custo")]
        public string NomeCentroCusto { get; set; }
        [Display(Name = "Direção")]
        public DirecaoCategoria Direcao { get; set; }
        [Display(Name = "Conta")]
        public string NomeConta { get; set; }
    }
}
