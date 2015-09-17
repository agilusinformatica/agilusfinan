using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Interfaces;
using AgilusFinan.Infra.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public class RepositorioPerfil : RepositorioPadrao<Perfil>, IRepositorioPerfil
    {
        public override void PreAlteracao(Perfil obj)
        {
            base.PreAlteracao(obj);

            List<Acesso> acessosDb = db.Acessos.Where(a => a.PerfilId == obj.Id).ToList();
            var exclusao = acessosDb.Where(t1 => !obj.Acessos.Any(t2 => t2.FuncaoId == t1.FuncaoId && t2.PerfilId == t1.PerfilId));
            var inclusao = obj.Acessos.Where(t1 => !acessosDb.Any(t2 => t2.FuncaoId == t1.FuncaoId && t2.PerfilId == t1.PerfilId));

            foreach (var acesso in exclusao)
            {
                db.Acessos.Remove(acesso);
            }

            foreach (var acesso in inclusao)
            {
                db.Acessos.Add(acesso);
            }

            obj.Acessos = null;
        }
    }
}
