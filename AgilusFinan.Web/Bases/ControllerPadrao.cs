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
            string folder = FolderViewName();
            PreListagem();
            if (folder == String.Empty)
            {
                return View(repo.Listar());    
            }
            else
                return View("~/Views/"+folder+"/Index.cshtml", repo.Listar());    
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            PreInclusao();
            ViewBag.TipoOperacao = "Incluindo";
            return View(new T());
        }

        [HttpPost]
        public virtual ActionResult Create(T model)
        {
            if (ModelState.IsValid)
            {
                repo.Incluir(model);
                return RedirectToAction("Index");
            }
            PreInclusao();
            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            PreAlteracao();
            T model = repo.BuscarPorId(id);
            ViewBag.TipoOperacao = "Alterando";
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Edit(T model)
        {
            if (ModelState.IsValid)
            {
                repo.Alterar(model);
                return RedirectToAction("Index");
            }
            PreAlteracao();
            return View(model);
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

        protected virtual string FolderViewName()
        {
            return "";
        }

    }
}

//enum TipoOperacao { Incluindo, Alterando}