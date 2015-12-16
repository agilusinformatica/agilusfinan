using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AgilusFinan.Domain.Entities
{
    public class Pessoa : Padrao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [Display(Name = "CPF/CNPJ")]
        public string Cpf { get; set; }
        public string Rg { get; set; }
        [Display(Name="Data de Nascimento")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNascimento { get; set; }
        [Display(Name="Endereço")]
        public Endereco Endereco { get; set; }
        [Display(Name="Conta Bancária")]
        public ContaBancaria ContaBancaria { get; set; }
        public virtual IList<TelefonePessoa> Telefones { get; set; }
        public virtual IList<TipoPessoaPorPessoa> TiposPessoa { get; set; }
        public string EmailContato { get; set; }
        public string EmailFinanceiro { get; set; }


        public Pessoa()
        {
            Endereco = new Endereco();
            ContaBancaria = new ContaBancaria();
            Telefones = new List<TelefonePessoa>();
            TiposPessoa = new List<TipoPessoaPorPessoa>();
        }

    }
}
