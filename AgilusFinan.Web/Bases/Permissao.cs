using AgilusFinan.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Bases
{
    public class Permissao : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //called before a controller action is executed 
            Dictionary<string, string> parametros;
            var controllerName = filterContext.RouteData.Values["controller"];
            var actionName = filterContext.RouteData.Values["action"];

            foreach (var parameter in filterContext.ActionParameters)
            {
                parametros = new Dictionary<string, string>();
                parametros.Add(parameter.Key.ToString(), parameter.Value.ToString());
            }

        }
    }
}