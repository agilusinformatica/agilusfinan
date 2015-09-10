﻿using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public static class Login
    {
        public static void ValidaLogin(string usuario, string senha)
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

                UsuarioLogado.UsuarioId = usu[0].Id;
                UsuarioLogado.PerfilId = usu[0].PerfilId;
                UsuarioLogado.EmpresaId = usu[0].EmpresaId;
            }
        }

        public static string Encode(string value)
        {
            var hash = System.Security.Cryptography.SHA256.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }
    }
}