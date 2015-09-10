using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgilusFinan.Domain.Entities;

namespace AgilusFinan.Web.Model
{
    public static class Sessao
    {
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
            set { HttpContext.Current.Session["IdUsuarioLogado"] = value; }
        }
        public static int PerfilId { get { return (int)HttpContext.Current.Session["IdPerfilLogado"]; } set { HttpContext.Current.Session["IdPerfilLogado"] = value; } }
        public static int EmpresaId { get { return (int)HttpContext.Current.Session["IdEmpresaLogada"]; } set { HttpContext.Current.Session["IdEmpresaLogada"] = value; } }
    }
}