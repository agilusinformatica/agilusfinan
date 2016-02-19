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
                if (HttpContext.Current.Session["NomeUsuarioLogado"] != null)
                {
                    retorno = (string)HttpContext.Current.Session["NomeUsuarioLogado"];
                }
                else
                {
                    retorno = String.Empty;
                }
                return retorno;
            }
            set 
            { 
                HttpContext.Current.Session["NomeUsuarioLogado"] = value; 
            }
        }

        public static string NomePerfil
        {
            get
            {
                string retorno;
                if (HttpContext.Current.Session["NomePerfilLogado"] != null)
                {
                    retorno = (string)HttpContext.Current.Session["NomePerfilLogado"];
                }
                else
                {
                    retorno = String.Empty;
                }
                return retorno;
            }
            set 
            { 
                HttpContext.Current.Session["NomePerfilLogado"] = value; 
            }
        }

        public static int UsuarioId
        {
            get
            {
                int retorno;
                if (HttpContext.Current.Session["IdUsuarioLogado"] != null)
                {
                    retorno = (int)HttpContext.Current.Session["IdUsuarioLogado"];
                }
                else
                {
                    retorno = 0;
                }
                return retorno;
            }
            set 
            { 
                HttpContext.Current.Session["IdUsuarioLogado"] = value; 
            }
        }

        public static int PerfilId { 
            get 
            { 
                return (int)HttpContext.Current.Session["IdPerfilLogado"];
            } 
            set 
            { 
                HttpContext.Current.Session["IdPerfilLogado"] = value; 
            } 
        }

        public static int EmpresaId { 
            get 
            {
                var sessao = HttpContext.Current.Session;
                int? empresaId = null;
                if( sessao != null)
                    empresaId = (int?)sessao["IdEmpresaLogada"];

                if (empresaId != null)
                    return (int)empresaId;
                else
                    return 0;
                
            } 
            set 
            { 
                HttpContext.Current.Session["IdEmpresaLogada"] = value; 
            } 
        }
        public static List<Funcao> Acessos { get; set; }
    }
}
