using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgilusFinan.Web.ViewModels
{
    public class PessoaViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Informe o CPF/CNPJ")]
        [Display(Name = "CPF/CNPJ")]
        public string Cpf { get; set; }
        [Display(Name = "RG")]
        public string Rg { get; set; }
        [Display(Name = "Data de Nascimento")]
        public DateTime? DataNascimento { get; set; }
        public Endereco Endereco { get; set; }
        [Display(Name = "E-mail de Contato")]
        public string EmailContato { get; set; }
        [Display(Name = "E-mail Financeiro")]
        public string EmailFinanceiro { get; set; }
        public ContaBancaria ContaBancaria { get; set; }
        public List<int> TiposPorPessoa { get; set; }
        public List<TelefonePessoaViewModel> Telefones { get; set; }

        public PessoaViewModel()
        {
            Endereco = new Endereco();
            ContaBancaria = new ContaBancaria();
            Telefones = new List<TelefonePessoaViewModel>();
            TiposPorPessoa = new List<int>();
        }
    }


    public class TelefonePessoaViewModel
    {
        public int Id { get; set; }

        public string Numero { get; set; }

        public string Ddd { get; set; }

        public int TipoTelefoneId { get; set; }
    }
}