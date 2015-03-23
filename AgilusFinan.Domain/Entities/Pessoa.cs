using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AgilusFinan.Domain.Entities
{
    public class Pessoa : Padrao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        [Display(Name="Data de Nascimento")]
        public DateTime DataNascimento { get; set; }
        [Display(Name="Endereço")]
        public Endereco Endereco { get; set; }
        [Display(Name="Conta Bancária")]
        public ContaBancaria ContaBancaria { get; set; }
        public virtual IEnumerable<TelefonePessoa> Telefones { get; set; }
        public virtual IEnumerable<TipoPessoaPorPessoa> TiposPessoa { get; set; }

        public Pessoa()
        {
            Endereco = new Endereco();
            ContaBancaria = new ContaBancaria();
            Telefones = new List<TelefonePessoa>();
            TiposPessoa = new List<TipoPessoaPorPessoa>();
        }

    }
}
