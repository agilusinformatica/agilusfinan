using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Interfaces;
using System.Linq;

namespace AgilusFinan.Infra.Services
{
    public class RepositorioPessoa : RepositorioPadrao<Pessoa>, IRepositorioPessoa
    {
        public override void PreAlteracao(Pessoa obj)
        {
            base.PreAlteracao(obj);
            var telefones = db.TelefonesPessoa.Where(t => t.PessoaId == obj.Id);
            var tipos = db.TiposPessoaPorPessoa.Where(t => t.PessoaId == obj.Id);

            foreach (var telefone in telefones)
            {
                db.TelefonesPessoa.Remove(telefone);
            }

            foreach (var tipo in tipos)
            {
                db.TiposPessoaPorPessoa.Remove(tipo);
            }

            foreach (var telefone in obj.Telefones)
            {
                db.TelefonesPessoa.Add(telefone);
            }

            foreach (var tipo in obj.TiposPessoa)
            {
                db.TiposPessoaPorPessoa.Add(tipo);
            }
        }
    }
}
