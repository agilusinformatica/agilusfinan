using System.Web.Mvc;
using AgilusFinan.Domain.Interfaces;
using AgilusFinan.Domain.Entities;
using System.Web.Script.Serialization;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace AgilusFinan.Web.Bases
{
    public class ControllerViewModelPadrao<T, R, V> : Controller
        where T : Padrao, new()
        where R : IRepositorioPadrao<T>, new()
        where V : class, new()
    {
        protected R repo = new R();

        protected virtual IEnumerable<T> Dados()
        {
            return repo.Listar();
        }

        [Permissao]
        public virtual ActionResult Index()
        {
            PreListagem();
            return FolderViewName() == String.Empty ? View(Dados()) : View("~/Views/" + FolderViewName() + "/Index.cshtml", Dados());
        }

        [HttpGet]
        [Permissao]
        public virtual ActionResult Create()
        {
            T model = new T();
            PreInclusao();
            ViewBag.TipoOperacao = "Incluindo";
            V viewModel = new V();
            ModelToViewModel(model, viewModel);
            return FolderViewName() == String.Empty ? View(viewModel) : View("~/Views/" + FolderViewName() + "/Create.cshtml", viewModel);
        }

        [Permissao]
        [HttpPost]
        public virtual void Create(string postedData)
        {
            var js = new JavaScriptSerializer();
            V viewModel = js.Deserialize<V>(postedData);
            var _model = new T();
            ViewModelToModel(viewModel, _model);
            repo.Incluir(_model);
          
            TempData["Alerta"] = new Alerta() { Mensagem = "Registro gravado com sucesso", Tipo = "success" };
        }

        public virtual void Erro(string erro)
        {
            throw new Exception(erro);
        }

        [HttpGet]
        [Permissao]
        public virtual ActionResult Edit(int id)
        {
            T model = repo.BuscarPorId(id);
            ViewBag.TipoOperacao = "Alterando";
            V viewModel = new V();
            ModelToViewModel(model, viewModel);
            PreAlteracao(viewModel);
            return FolderViewName() == String.Empty ? View(viewModel) : View("~/Views/" + FolderViewName() + "/Edit.cshtml", viewModel);
        }

        [HttpGet]
        public virtual ActionResult Duplicar(int id)
        {
            T model = repo.BuscarPorId(id);
            ViewBag.TipoOperacao = "Incluindo";
            V viewModel = new V();
            ModelToViewModel(model, viewModel);
            PreAlteracao(viewModel);
            return FolderViewName() == String.Empty ? View(viewModel) : View("~/Views/" + FolderViewName() + "/Duplicar.cshtml", viewModel);
        }
        [HttpPost]
        [Permissao]
        public virtual ActionResult Edit(string postedData)
        {
            var js = new JavaScriptSerializer();
            V viewModel = js.Deserialize<V>(postedData);
            var _model = new T();
            ViewModelToModel(viewModel, _model);
            if (ModelState.IsValid)
            {
                repo.Alterar(_model);

                return RedirectToAction("Index");
            }
            PreAlteracao(viewModel);
            return FolderViewName() == String.Empty ? View(viewModel) : View("~/Views/" + FolderViewName() + "/Edit.cshtml", viewModel);
        }

        [HttpGet]
        [Permissao]
        public virtual ActionResult Delete(int id)
        {
            PreExclusao();
            T model = repo.BuscarPorId(id);
            return FolderViewName() == String.Empty ? View(model) : View("~/Views/" + FolderViewName() + "/Delete.cshtml", model);
        }

        [HttpPost, ActionName("Delete")]
        [Permissao]
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