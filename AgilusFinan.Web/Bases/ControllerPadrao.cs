using System.Web.Mvc;
using AgilusFinan.Domain.Interfaces;
using AgilusFinan.Domain.Entities;
using System;

namespace AgilusFinan.Web.Bases
{
    public class ControllerPadrao<T, R> : Controller 
        where T : Padrao, new() 
        where R : IRepositorioPadrao<T>, new()
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
            return FolderViewName() == String.Empty ? View(new T()) : View("~/Views/" + FolderViewName() + "/Create.cshtml", new T());

        }

        [HttpPost]
        [Permissao]
        public virtual ActionResult Create(T model)
        {
            if (ModelState.IsValid)
            {
                repo.Incluir(model);
                return RedirectToAction("Index");
            }
            PreInclusao();
            return FolderViewName() == String.Empty ? View(model) : View("~/Views/" + FolderViewName() + "/Create.cshtml", model);

        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            T model = repo.BuscarPorId(id);
            PreAlteracao(model);
            ViewBag.TipoOperacao = "Alterando";
            return FolderViewName() == String.Empty ? View(model) : View("~/Views/" + FolderViewName() + "/Edit.cshtml", model);

        }

        [HttpPost]
        public virtual ActionResult Edit(T model)
        {
            if (ModelState.IsValid)
            {
                repo.Alterar(model);
                return RedirectToAction("Index");
            }
            PreAlteracao(model);
            return FolderViewName() == String.Empty ? View(model) : View("~/Views/" + FolderViewName() + "/Edit.cshtml", model);

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

        protected virtual void PreAlteracao(T model)
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

        protected virtual string FolderViewName()
        {
            return string.Empty;
        }

    }
}

