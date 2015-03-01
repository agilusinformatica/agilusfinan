using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgilusFinan.Infra.Context;

namespace AgilusFinan.Infra.Services
{
    public class RepositorioBanco : RepositorioPadrao<Banco>, IRepositorioBanco
    {
        private readonly Contexto db = new Contexto();
        public IEnumerable<Banco> Listar2(Func<Banco, bool> predicate)
        {
            return db.Bancos.Where(predicate).ToList();
        }
    }
}
