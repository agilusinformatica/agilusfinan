using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Utils;
using AgilusFinan.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public static class Login
    {
        public static Usuario ValidaLogin(string usuario, string senha)
        {
            using (var db = new Contexto())
            {
                var usu = db.Usuarios.Where(u => u.Email == usuario && u.Senha == senha).ToList();
                if (usu.Count == 0)
                    throw new Exception("Usuário ou Senha inválidos");

                if (!usu[0].Ativo)
                {
                    throw new Exception("Usuário não está ativo");
                }

                if ((senha == null) || senha.Trim() == String.Empty)
                {
                    throw new Exception("Senha não pode ser em branco");
                }
                return usu[0];
            }
            
        }


    }
}
