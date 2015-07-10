using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Interfaces;

namespace AgilusFinan.Infra.Services
{
    public class RepositorioTituloRecorrente : RepositorioPadrao<TituloRecorrente>, IRepositorioTituloRecorrente
    {
        public override void PreInclusao(TituloRecorrente obj)
        {
            base.PreInclusao(obj);
            obj.DataCadastro = DateTime.Now;
        }
    }
}
