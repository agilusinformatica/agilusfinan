using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Interfaces;
using AgilusFinan.Infra.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public class RepositorioPerfil : RepositorioPadrao<Perfil>, IRepositorioPerfil
    {
        public override void PreAlteracao(Perfil obj)
        {
            base.PreAlteracao(obj);
            var acessos = db.Acessos.Where(a => a.PerfilId== obj.Id);

            foreach (var acesso in acessos)
            {
                db.Acessos.Remove(acesso);
            }

            foreach (var acesso in obj.Acessos)
            {
                db.Acessos.Add(acesso);
            }
        }
    }
}
