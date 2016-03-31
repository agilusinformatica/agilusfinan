using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public class RepositorioEmpresa
    {
        Contexto db = new Contexto();

        public Empresa BuscarPorId(int id)
        {
            return db.Empresas.FirstOrDefault(e => e.Id == id);
        }

        public void Edit(Empresa obj)
        {
            obj.Id = UsuarioLogado.EmpresaId;
            db.Entry<Empresa>(obj).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
