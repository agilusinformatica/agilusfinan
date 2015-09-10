using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public static class ListarAcessosUsuario
    {
        public static List<Funcao> ChamarFuncaoAcesso(int UsuarioId)
        {
            List<Funcao> Funcoes;

            using (Contexto context = new Contexto())
            {
                Funcoes = context.Database.SqlQuery<Funcao>("pr_listar_acessos @UsuarioId",
                new SqlParameter("@UsuarioId", UsuarioId)).ToList();
            }
            return Funcoes;
        }
    }
}
