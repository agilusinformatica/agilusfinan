using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public class RepositorioEmpresa
    {
        public Empresa BuscarPorId(int id)
        {
            Contexto db = new Contexto();

            return db.Empresas.FirstOrDefault(e => e.Id == id);
        }
    }
}
