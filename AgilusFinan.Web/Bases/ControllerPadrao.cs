using System.Web.Mvc;
using AgilusFinan.Domain.Interfaces;
using AgilusFinan.Infra.Services;

namespace AgilusFinan.Web.Bases
{
    public class ControllerPadrao<T, R> : Controller where T : class where R : IRepositorioPadrao<T>, new()
    {
        protected R repo = new R();
        
        public ActionResult Index()
        {
            return View(repo.Listar());
        }

        [HttpGet]
        public ActionResult Create()
        {
            PreInclusao();
            return View();
        }

        [HttpPost]
        public ActionResult Create(T model)
        {
            if (ModelState.IsValid)
            {
                repo.Incluir(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            PreAlteracao();
            T model = repo.BuscarPorId(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(T model)
        {
            if (ModelState.IsValid)
            {
                repo.Alterar(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            PreExclusao();
            T model = repo.BuscarPorId(id);
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
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
    }
}