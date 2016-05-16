using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Areas.Bases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Areas.Api.Controllers
{
    
    public class BancoController : Controller
    {
        // GET: Api/Banco
        public string Index()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ContractResolver = new CustomResolver();
            settings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.Formatting = Formatting.Indented;

            // Serialização customizada, ignorando propriedades de navegação das classes 
            string json = JsonConvert.SerializeObject(new RepositorioBanco().Listar(), settings);
            return json;
        }
    }
}