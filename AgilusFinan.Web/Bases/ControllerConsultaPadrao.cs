using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AgilusFinan.Infra.Services;
using AgilusFinan.Domain.Entities;

namespace AgilusFinan.Web.Bases
{
    public class ControllerConsultaPadrao<T, C> : Controller
        where T : IConsulta<C>, new()
        where C : ConsultaPadrao, new()
    {
        protected T gerador = new T();

        public ActionResult Index()
        {
            PreFiltro();
            return View("~/Views/Relatorio/Index.cshtml", gerador.DefineFiltro());
        }

        [HttpPost]
        public ActionResult Consulta(string jsonFiltros)
        {
            Filtro filtro = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Filtro>(jsonFiltros);
            List<C> dados = gerador.ChamarProcedimento(filtro);
            return View("~/Views/" + FolderViewName() + "/Index.cshtml", dados);
        }

        public virtual string FolderViewName()
        {
            return String.Empty;
        }

        public virtual void PreFiltro()
        {

        }
    }
}
