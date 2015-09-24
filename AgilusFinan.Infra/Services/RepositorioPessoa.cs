using System;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AgilusFinan.Infra.Services
{
    public class RepositorioPessoa : RepositorioPadrao<Pessoa>, IRepositorioPessoa
    {
        public override void PreAlteracao(Pessoa obj)
        {
            base.PreAlteracao(obj);
            var telefones = db.TelefonesPessoa.Where(t => t.PessoaId == obj.Id).ToList();
            var exclusaoTelefones = telefones.Where(t => !obj.Telefones.Any(o => t.Telefone.Numero == o.Telefone.Numero));
            var insercaoTelefones = obj.Telefones.Where(o => !telefones.Any(t => o.Telefone.Numero == t.Telefone.Numero));

            var alteracaoTelefones = from telefone in telefones
                                     join o in obj.Telefones on telefone.Telefone.Numero equals o.Telefone.Numero
                                     where o.Telefone.Ddd != telefone.Telefone.Ddd || o.Telefone.TipoTelefoneId != telefone.Telefone.TipoTelefoneId
                                     select o;

            foreach (var telefone in exclusaoTelefones)
            {
                db.TelefonesPessoa.Remove(telefone);
            }

            foreach (var telefone in insercaoTelefones)
            {
                db.TelefonesPessoa.Add(telefone);
            }

            foreach (var telefone in alteracaoTelefones)
            {
                var original = db.TelefonesPessoa.Find(telefone.Id);
                
                if(original != null)
                {
                    db.Entry(original).CurrentValues.SetValues(telefone);
                }
            }

            obj.Telefones = null;

            var tipos = db.TiposPessoaPorPessoa.Where(t => t.PessoaId == obj.Id).ToList();
            var exclusaoTipos = tipos.Where(t => !obj.TiposPessoa.Any(o => t.TipoPessoaId == o.TipoPessoaId)).ToList();
            var insercaoTipos = obj.TiposPessoa.Where(o => !tipos.Any(t => o.TipoPessoaId == t.TipoPessoaId)).ToList();

            foreach (var tipo in exclusaoTipos)
            {
                db.TiposPessoaPorPessoa.Remove(tipo);
            }

            foreach (var tipo in insercaoTipos)
            {
                db.TiposPessoaPorPessoa.Add(tipo);
            }

            obj.TiposPessoa = null;

        }
    }
}