using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public static class AcessosUsuario
    {
        public static List<Funcao> Listar(int usuarioId)
        {
            using (var db = new Contexto())
            {
                return db.Database.SqlQuery<Funcao>("pr_listar_acessos @UsuarioId", new SqlParameter("@UsuarioId", usuarioId)).ToList();
            }
        }
    }
}
