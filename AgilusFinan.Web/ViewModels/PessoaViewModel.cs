using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using System;
using System.Collections.Generic;

namespace AgilusFinan.Web.ViewModels
{
    public class PessoaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime DataNascimento { get; set; }
        public Endereco Endereco { get; set; }
        public ContaBancaria ContaBancaria { get; set; }

        public List<TipoPorPessoa> TiposPorPessoa { get; set; }
        public List<Telefone> Telefones { get; set; }

        public PessoaViewModel()
        {
            Endereco = new Endereco();
            ContaBancaria = new ContaBancaria();
            Telefones = new List<Telefone>();
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
}