using System.Web.Mvc;
using AgilusFinan.Domain.Interfaces;

namespace AgilusFinan.Web.Bases
{
    public class ControllerPadrao<T, R> : Controller where T : class, new() where R : IRepositorioPadrao<T>, new()
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
            T model = new T();
            ViewBag.TipoOperacao = "Incluindo";
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Create(T model)
        {
            if (ModelState.IsValid)
            {
                repo.Incluir(model);
                return RedirectToAction("Index");
            }
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
    }
}

enum TipoOperacao { Incluindo, Alterando}