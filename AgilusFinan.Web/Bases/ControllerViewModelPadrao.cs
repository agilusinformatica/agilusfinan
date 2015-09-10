using System.Web.Mvc;
using AgilusFinan.Domain.Interfaces;
using AgilusFinan.Domain.Entities;
using System.Web.Script.Serialization;
using System;

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
            return FolderViewName() == String.Empty ? View(repo.Listar()) : View("~/Views/" + FolderViewName() + "/Index.cshtml", repo.Listar());

        }

        [HttpGet]
        [Permissao]
        public virtual ActionResult Create()
        {
            PreInclusao();
            ViewBag.TipoOperacao = "Incluindo";
            return FolderViewName() == String.Empty ? View(new V()) : View("~/Views/" + FolderViewName() + "/Create.cshtml", new V());
        }

        public virtual ActionResult Create(string postedData)
        {
            var js = new JavaScriptSerializer();
            V viewModel = js.Deserialize<V>(postedData);
            if (ModelState.IsValid)
            {
                var _model = new T();
                ViewModelToModel(viewModel, _model);
                repo.Incluir(_model);

                return RedirectToAction("Index");
            }
            PreInclusao();
            return FolderViewName() == String.Empty ? View(viewModel) : View("~/Views/" + FolderViewName() + "/Create.cshtml", viewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            T model = repo.BuscarPorId(id);
            ViewBag.TipoOperacao = "Alterando";
            V viewModel = new V();
            ModelToViewModel(model, viewModel);
            PreAlteracao(viewModel);
            return FolderViewName() == String.Empty ? View(viewModel) : View("~/Views/" + FolderViewName() + "/Edit.cshtml", viewModel);

        }

        [HttpPost]
        public virtual ActionResult Edit(string postedData)
        {
            var js = new JavaScriptSerializer();
            V viewModel = js.Deserialize<V>(postedData);
            if (ModelState.IsValid)
            {
                var _model = new T();
                ViewModelToModel(viewModel, _model);
                repo.Alterar(_model);
                
                return RedirectToAction("Index");
            }
            PreAlteracao(viewModel);
            return FolderViewName() == String.Empty ? View(viewModel) : View("~/Views/" + FolderViewName() + "/Edit.cshtml", viewModel);
        }

        [HttpGet]
        public virtual ActionResult Delete(int id)
        {
            PreExclusao();
            T model = repo.BuscarPorId(id);
            return FolderViewName() == String.Empty ? View(model) : View("~/Views/" + FolderViewName() + "/Delete.cshtml", model);


        }

        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            repo.ExcluirPorId(id);
            return RedirectToAction("Index");
        }

        protected virtual void PreAlteracao(V viewModel)
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

        protected virtual string FolderViewName()
        {
            return string.Empty;
        }

    }
}