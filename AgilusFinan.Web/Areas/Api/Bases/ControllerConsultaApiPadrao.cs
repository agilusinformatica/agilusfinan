using System;
using System.Collections.Generic;
using AgilusFinan.Infra.Services;
using AgilusFinan.Domain.Entities;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgilusFinan.Web.Bases;
using System.Collections.Specialized;
using Newtonsoft.Json;
using AgilusFinan.Web.Areas.Api.Controllers;

namespace AgilusFinan.Web.Areas.Api.Bases
{
    public abstract class ControllerConsultaApiPadrao<T, C> : Controller
        where T : IConsulta<C>, new()
        where C : ConsultaPadrao, new()
    {
        protected T gerador = new T();

        [AllowAnonymous]
        public string Consulta()
        {
            var HeaderParams = HttpContext.Request.Params;
            TokenController.ValidarToken(HeaderParams.Get("token"));
            try
            {
                List<ParametroFiltro> pFiltro = new List<ParametroFiltro>();

                Filtro filtro = DefineTipoFiltro(HeaderParams);
                List<C> dados = gerador.ChamarProcedimento(filtro);
                return JsonConvert.SerializeObject(dados);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                UsuarioLogado.ExpiraCookie();
            }
        }

        public abstract Filtro DefineTipoFiltro(NameValueCollection parametros);

        public virtual void PreFiltro()
        {

        }
    }
}