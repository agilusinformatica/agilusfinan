using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgilusFinan.Web.ViewModels
{
    public class PessoaViewModel : ViewModel<Pessoa>
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
        [Display(Name = "Tipo da Pessoa")]
        public List<int> TiposPorPessoa { get; set; }
        public List<TelefonePessoaViewModel> Telefones { get; set; }

        public PessoaViewModel()
        {
            Endereco = new Endereco();
            ContaBancaria = new ContaBancaria();
            Telefones = new List<TelefonePessoaViewModel>();
            TiposPorPessoa = new List<int>();
        }



        public class TelefonePessoaViewModel
        {
            public int Id { get; set; }
            public string Numero { get; set; }
            public string Ddd { get; set; }
            public int TipoTelefoneId { get; set; }
        }


        public void FromModel(Pessoa model)
        {
            this.ContaBancaria = model.ContaBancaria;
            this.Cpf = model.Cpf;
            this.DataNascimento = model.DataNascimento;
            this.EmailContato = model.EmailContato;
            this.EmailFinanceiro = model.EmailFinanceiro;
            this.Endereco = model.Endereco;
            this.Nome = model.Nome;
            this.Rg = model.Rg;

            foreach (var t in model.Telefones)
            {
                this.Telefones.Add(new TelefonePessoaViewModel() { Ddd = t.Telefone.Ddd, Numero = t.Telefone.Numero, TipoTelefoneId = t.Telefone.TipoTelefoneId, Id = t.Id });
            }

            foreach (var tp in model.TiposPessoa)
            {
                this.TiposPorPessoa.Add(tp.TipoPessoaId);
            }
        }

        public Pessoa ToModel()
        {
            Pessoa pessoa = new Pessoa();
            pessoa.ContaBancaria = this.ContaBancaria;
            pessoa.Id = this.Id;
            pessoa.Cpf = this.Cpf;
            pessoa.DataNascimento = this.DataNascimento;
            pessoa.EmailFinanceiro = this.EmailFinanceiro;
            pessoa.EmailContato = this.EmailContato;
            pessoa.Endereco = this.Endereco;
            pessoa.Nome = this.Nome;
            pessoa.Rg = this.Rg;

            foreach (var t in this.Telefones)
            {
                pessoa.Telefones.Add(new TelefonePessoa() 
                { 
                    Id = t.Id,
                    PessoaId = this.Id, 
                    Telefone = new Telefone() 
                    {
                        Ddd = t.Ddd, 
                        Numero = t.Numero, 
                        TipoTelefoneId = t.TipoTelefoneId 
                    } 
                });
            }

            foreach (var tp in this.TiposPorPessoa)
            {
                pessoa.TiposPessoa.Add(new TipoPessoaPorPessoa() { TipoPessoaId = tp, PessoaId = this.Id });
            }

            return pessoa;
        }
    }
}