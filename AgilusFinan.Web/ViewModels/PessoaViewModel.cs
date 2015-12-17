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
        public string Nome { get; set; }
        [Display(Name = "CPF/CNPJ")]
        public string Cpf { get; set; }
        public string Rg { get; set; }
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }
        public Endereco Endereco { get; set; }
        public string EmailContato { get; set; }
        public string EmailFinanceiro { get; set; }
        public ContaBancaria ContaBancaria { get; set; }

        public List<TipoPorPessoa> TiposPorPessoa { get; set; }
        public List<TelefonePessoaViewModel> Telefones { get; set; }

        public PessoaViewModel()
        {
            Endereco = new Endereco();
            ContaBancaria = new ContaBancaria();
            Telefones = new List<TelefonePessoaViewModel>();
            TiposPorPessoa = new List<TipoPorPessoa>();

            foreach (var tipo in new RepositorioTipoPessoa().Listar())
            {
                TiposPorPessoa.Add(new TipoPorPessoa()
                {
                    Id = tipo.Id,
                    Nome = tipo.Nome,
                    Marcado = false

                });
            }
        }
    }

    public class TipoPorPessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Marcado { get; set; }
    }

    public class TelefonePessoaViewModel
    {
        public int Id { get; set; }
        //public Telefone Telefone { get; set; }

        public string Numero { get; set; }

        public string Ddd { get; set; }

        public int TipoTelefoneId { get; set; }
    }
}