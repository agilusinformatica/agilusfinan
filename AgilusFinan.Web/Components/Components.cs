using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Components
{
    public static class Components
    {
        public static MvcHtmlString BotaoEnviar(this HtmlHelper helper, string buttonText)
        {
            string str = "<input type=\"submit\" value=\"" + buttonText + "\" />";
            return new MvcHtmlString(str);
        }
    }
}