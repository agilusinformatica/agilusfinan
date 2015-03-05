using System.Web.Mvc;
using AgilusFinan.Web.Bases;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;

namespace AgilusFinan.Web.Controllers
{
    public class CentroCustoController : ControllerPadrao<CentroCusto, RepositorioCentroCusto>
    {
        protected override void PreInclusao()
        {
            base.PreInclusao();
            ViewBag.EmpresaId = new SelectList(new RepositorioEmpresa().Listar(), "Id", "Nome");
        }

        protected override void PreAlteracao()
        {
            base.PreAlteracao();
            ViewBag.EmpresaId = new SelectList(new RepositorioEmpresa().Listar(), "Id", "Nome");
        }

    }
}