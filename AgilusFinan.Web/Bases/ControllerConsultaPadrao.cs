using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AgilusFinan.Infra.Services;
using AgilusFinan.Domain.Entities;
using Newtonsoft.Json;

namespace AgilusFinan.Web.Bases
{
    public class ControllerConsultaPadrao<T, C> : Controller
        where T : IConsulta<C>, new()
        where C : ConsultaPadrao, new()
    {
        protected T gerador = new T();

        [Permissao]
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
            ViewResult pagina;

            if (NomeCache() != null)
            {
                var parametros = new Dictionary<string, string>();

                parametros.Add("empresaId", UsuarioLogado.EmpresaId.ToString());

                foreach (var item in filtro.Parametros)
                {
                    if (item.Tipo == TipoFiltro.data)
                    {
                        var dataFiltro = Convert.ToDateTime(item.Valor);
                        parametros.Add(item.Nome, dataFiltro.ToString("dd/MM/yyyy"));
                    }
                    else
                        parametros.Add(item.Nome, item.Valor);
                }


                pagina = (ViewResult)Cache.Busca(NomeCache(), parametros);

                if (pagina == null)
                {
                    pagina = View("~/Views/" + FolderViewName() + "/Index.cshtml", dados);
                    Cache.Insere(NomeCache(), parametros, pagina);
                }
                else
                    return pagina;
            }
           
            return View("~/Views/" + FolderViewName() + "/Index.cshtml", dados);
        }

        [HttpPost]
        public string ConsultaApi(string jsonFiltros)
        {
            Filtro filtro = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Filtro>(jsonFiltros);
            List<C> dados = gerador.ChamarProcedimento(filtro);
            return JsonConvert.SerializeObject(dados);
        }

        public virtual string FolderViewName()
        {
            return String.Empty;
        }

        public virtual void PreFiltro()
        {

        }

        public virtual string NomeCache()
        {
            return null;
        }
    }
}
