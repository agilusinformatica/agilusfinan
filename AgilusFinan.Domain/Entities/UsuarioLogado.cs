using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AgilusFinan.Domain.Entities
{
    public static class UsuarioLogado
    {
        public static string Nome
        {
            get
            {
                string retorno;
                if (HttpContext.Current.Request.Cookies["NomeUsuarioLogado"] != null)
                {
                    retorno = HttpContext.Current.Request.Cookies["NomeUsuarioLogado"].Value;
                }
                else
                {
                    retorno = String.Empty;
                }
                return retorno;
            }
            set 
            {
                HttpCookie cookie = new HttpCookie("NomeUsuarioLogado", value);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public static string NomePerfil
        {
            get
            {
                string retorno;
                if (HttpContext.Current.Request.Cookies["NomePerfilLogado"] != null)
                {
                    retorno = HttpContext.Current.Request.Cookies["NomePerfilLogado"].Value;
                }
                else
                {
                    retorno = String.Empty;
                }
                return retorno;
            }
            set 
            { 
                HttpCookie cookie = new HttpCookie("NomePerfilLogado", value);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public static int UsuarioId
        {
            get
            {
                int retorno;
                if (HttpContext.Current.Request.Cookies["IdUsuarioLogado"] != null)
                {
                    retorno = Convert.ToInt32(HttpContext.Current.Request.Cookies["IdUsuarioLogado"].Value);
                }
                else
                {
                    retorno = 0;
                }
                return retorno;
            }
            set 
            { 
                HttpCookie cookie = new HttpCookie("IdUsuarioLogado", value.ToString());
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public static int PerfilId 
        { 
            get 
            { 
                return Convert.ToInt32(HttpContext.Current.Request.Cookies["IdPerfilLogado"].Value);
            } 
            set 
            { 
                HttpCookie cookie = new HttpCookie("IdPerfilLogado", value.ToString());
                HttpContext.Current.Response.Cookies.Add(cookie);
            } 
        }

        public static int EmpresaId 
        { 
            get 
            {
                var cookie = HttpContext.Current.Request.Cookies["IdEmpresaLogada"];
                int? empresaId = null;
                if (cookie != null && cookie.Value != String.Empty)
                    empresaId = Convert.ToInt32(HttpContext.Current.Request.Cookies["IdEmpresaLogada"].Value);

                if (empresaId != null)
                    return (int)empresaId;
                else
                    return 0;
                
            } 
            set 
            {
                HttpCookie cookie = new HttpCookie("IdEmpresaLogada", value.ToString());
                cookie.Expires = DateTime.Today.AddYears(1);
                HttpContext.Current.Response.Cookies.Set(cookie);
            } 
        }
    }
}
