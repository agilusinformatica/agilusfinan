using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;

namespace AgilusFinan.Web.Controllers
{
    public class ResumoTituloCategoriaController : Controller
    {
        // GET: ResumoTituloCategoria
        public PartialViewResult Index(DateTime dataInicial, DateTime dataFinal)
        {
            var identadas = from c in Util.CategoriasIdentadas(null)
                            join g in GeradorResumoTituloCategoria.ChamarProcedimentoResumoCategoria(dataInicial, dataFinal) on c.Key equals g.CategoriaId
                            select new { g.CategoriaId, c.Value, g.Cor, g.CategoriaPaiId, g.Valor };

            var lista = new List<ResumoTituloCategoria>();

            foreach (var c in identadas)
            {
                lista.Add(new ResumoTituloCategoria() { CategoriaId = c.CategoriaId, 
                                                        CategoriaPaiId = c.CategoriaPaiId, 
                                                        Cor = c.Cor, 
                                                        Nome = c.Value, 
                                                        Valor = c.Valor });
            }

            return PartialView("~/Views/ResumoTituloCategoria/_Index.cshtml", lista);
        }
    }
}