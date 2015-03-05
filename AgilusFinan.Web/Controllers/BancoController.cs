using System.Linq;
using System.Web.Mvc;
using AgilusFinan.Domain.Interfaces;
using AgilusFinan.Infra.Context;

namespace AgilusFinan.Web.Controllers
{
    public class BancoController : Controller
    {
        private readonly IRepositorioBanco _repo;

        public BancoController(IRepositorioBanco repositorio)
        {
            _repo = repositorio;
        }

        // GET: Banco
        public ActionResult Index()
        {
            return View(_repo.Listar(b => b.EmpresaId == 2));
            /*var db = new Contexto();
            return View(db.Bancos.Where(b => b.EmpresaId == 2).ToList());*/
        }

        // GET: Banco/Details/5
        public ActionResult Details(int id)
        {
            return View(_repo.BuscarPorId(id));
        }

        // GET: Banco/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Banco/Create
        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            try
            {
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Banco/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Banco/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Banco/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Banco/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
