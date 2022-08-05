using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public class RepositorioParametro : RepositorioPadrao<Parametro>, IRepositorioParametro
    {
        public override void PreAlteracao(Parametro obj)
        {
            base.PreAlteracao(obj);
            var tokenIugu = db.Parametros.Find(obj.Id).TokenIUGU;
            obj.TokenIUGU = tokenIugu;
        }
    }
}
