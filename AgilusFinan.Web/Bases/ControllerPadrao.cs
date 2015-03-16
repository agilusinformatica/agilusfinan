using System.Web.Mvc;
using AgilusFinan.Domain.Interfaces;
using AgilusFinan.Infra.Services;

namespace AgilusFinan.Web.Bases
{
    public class ControllerPadrao<T, R> : Controller where T : class where R : IRepositorioPadrao<T>, new()
    {
        protected R repo = new R();

        // Com esta annotation "Profile", caso seja necessário alterar as configurações de cache não é necessário recompilar a aplicação, basta alterar as configurações no web.config.
        [OutputCache(CacheProfile="IndexPage")]
        public virtual ActionResult Index()
        {
            //variável de teste
            var hora = System.DateTime.Now;
            ViewData.Add("horaServer", hora);

            return View(repo.Listar());
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            PreInclusao();
            return View();
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
    }
}