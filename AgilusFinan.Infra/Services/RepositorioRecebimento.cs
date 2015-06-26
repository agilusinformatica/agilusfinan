using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public class RepositorioRecebimento : RepositorioPadrao<Titulo>, IRepositorioTitulo
    {
        public override IEnumerable<Titulo> Listar()
        {
            return db.Set<Titulo>().Where(e => e.EmpresaId == db.EmpresaId && e.Categoria.Direcao == DirecaoCategoria.Recebimento);
        }

        public override List<Titulo> Listar(Expression<Func<Titulo, bool>> predicate)
        {
            return db.Set<Titulo>().Where(predicate).Where(e => e.EmpresaId == db.EmpresaId && e.Categoria.Direcao == DirecaoCategoria.Recebimento).ToList();
        }
    }
}
