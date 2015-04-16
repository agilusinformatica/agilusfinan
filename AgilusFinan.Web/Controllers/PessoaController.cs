using System.Web.Mvc;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using AgilusFinan.Web.ViewModels;

namespace AgilusFinan.Web.Controllers
{
    public class PessoaController : ControllerPadrao<Pessoa, RepositorioPessoa>
    {
        protected override void PreInclusao()
        {
            base.PreInclusao();
            ViewBag.ListaBancos = new RepositorioBanco().Listar();
        }

        protected override void PreAlteracao()
        {
            base.PreAlteracao();
            ViewBag.ListaBancos = new RepositorioBanco().Listar();
        }


        public virtual ActionResult Details(int id)
        {
            Pessoa p = repo.BuscarPorId(id);
            return View(p);
        }

        [HttpGet]
        public override ActionResult Create()
        {
            PreInclusao();
            PessoaViewModel model = new PessoaViewModel();
            ViewBag.TipoOperacao = "Incluindo";
            return View(model);
        }
    }
}