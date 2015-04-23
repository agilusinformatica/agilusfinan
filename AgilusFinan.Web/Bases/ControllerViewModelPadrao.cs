using System.Web.Mvc;
using AgilusFinan.Domain.Interfaces;
using AgilusFinan.Domain.Entities;

namespace AgilusFinan.Web.Bases
{
    public class ControllerViewModelPadrao<T, R, V> : Controller
        where T : Padrao, new() 
        where R : IRepositorioPadrao<T>, new()
        where V : class, new() 

    {
        protected R repo = new R();

        public virtual ActionResult Index()
        {
            PreListagem();
            return View(repo.Listar());
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            PreInclusao();
            ViewBag.TipoOperacao = "Incluindo";

            return View(new V());
        }

        [HttpPost]
        public virtual ActionResult Create(V viewModel)
        {
            if (ModelState.IsValid)
            {
                var _model = new T();
                ViewModelToModel(viewModel, _model);
                repo.Incluir(_model);

                return RedirectToAction("Index");
            }
            PreInclusao();
            return View(viewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            PreAlteracao();
            T model = repo.BuscarPorId(id);
            ViewBag.TipoOperacao = "Alterando";
            V viewModel = new V();
            ModelToViewModel(model, viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(V viewModel)
        {
            if (ModelState.IsValid)
            {
                var _model = new T();
                ViewModelToModel(viewModel, _model);
                repo.Alterar(_model);
                
                return RedirectToAction("Index");
            }
            PreAlteracao();
            return View(viewModel);
        }

        [HttpGet]
        public virtual ActionResult Delete(int id)
        {
            PreExclusao();
            T model = repo.BuscarPorId(id);
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            repo.ExcluirPorId(id);
            return RedirectToAction("Index");
        }

        protected virtual void PreAlteracao()
        {
            
        }

        protected virtual void PreInclusao()
        {
            
        }

        protected virtual void PreExclusao()
        {

        }

        protected virtual void PreListagem()
        {
            
        }

        protected virtual void ViewModelToModel(V viewModel, T model)
        {
            
        }

        protected virtual void ModelToViewModel(T model, V viewModel)
        {
            
        }

    }
}