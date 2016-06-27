using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace AgilusFinan.Domain.Entities
{
    public class Pessoa : Padrao
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime? DataNascimento { get; set; }
        public Endereco Endereco { get; set; }
        public ContaBancaria ContaBancaria { get; set; }
        public virtual IList<TelefonePessoa> Telefones { get; set; }
        public virtual IList<TipoPessoaPorPessoa> TiposPessoa { get; set; }
        public string EmailContato { get; set; }
        public string EmailFinanceiro { get; set; }
        public string Observacao { get; set; }


        public Pessoa()
        {
            Endereco = new Endereco();
            ContaBancaria = new ContaBancaria();
            Telefones = new List<TelefonePessoa>();
            TiposPessoa = new List<TipoPessoaPorPessoa>();
        }

    }
}
