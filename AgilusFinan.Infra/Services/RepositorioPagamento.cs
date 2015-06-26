using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public class RepositorioPagamento : RepositorioPadrao<Titulo>, IRepositorioTitulo
    {
        public override IEnumerable<Titulo> Listar()
        {
            return db.Set<Titulo>().Where(e => e.EmpresaId == db.EmpresaId && e.Categoria.Direcao == DirecaoCategoria.Pagamento);
        }

        public override List<Titulo> Listar(System.Linq.Expressions.Expression<Func<Titulo, bool>> predicate)
        {
            return db.Set<Titulo>().Where(predicate).Where(e => e.EmpresaId == db.EmpresaId && e.Categoria.Direcao == DirecaoCategoria.Pagamento).ToList();
        }
    }
}
