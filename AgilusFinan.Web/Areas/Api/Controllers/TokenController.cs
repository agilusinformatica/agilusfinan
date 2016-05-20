using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Utils;
using AgilusFinan.Infra.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AgilusFinan.Web.Areas.Api.Controllers
{
    public class TokenController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        public string GerarToken(string email, string senha)
        {
            try
            {
                Login.ValidaLogin(email, senha);
            }
            catch(Exception e)
            {
                return e.Message;
            }

            return Criptografia.Encriptar(email + "|" + senha);
        }


        public static void ValidarToken(string token)
        {
            var tokenD = Criptografia.Decriptar(token);
            var tokenL = tokenD.Split('|');
            string email = tokenL[0];
            string senha = tokenL[1];

            Usuario usu = Login.ValidaLogin(email, senha);
            UsuarioLogado.EmpresaId = usu.EmpresaId;
            Perfil prf = new RepositorioPerfil().BuscarPorId(usu.PerfilId);
            UsuarioLogado.PerfilId = usu.PerfilId;
            UsuarioLogado.UsuarioId = usu.Id;
            UsuarioLogado.Nome = usu.Nome;
            UsuarioLogado.NomePerfil = prf.Descricao;
        }
    }
}